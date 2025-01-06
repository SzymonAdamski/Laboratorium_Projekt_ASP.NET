using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers;

public class ProductionCompaniesController : Controller
{
    private readonly ApplicationDbContext _context;
    private const int PageSize = 10;

    public ProductionCompaniesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string searchString)
    {
        ViewData["CurrentSort"] = sortOrder;
        ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["CurrentFilter"] = searchString;

        var companies = _context.ProductionCompanies
            .Include(pc => pc.MovieProductionCompanies)
            .ThenInclude(mpc => mpc.Movie)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            companies = companies.Where(c => c.CompanyName.Contains(searchString));
        }

        companies = sortOrder switch
        {
            "name_desc" => companies.OrderByDescending(c => c.CompanyName),
            _ => companies.OrderBy(c => c.CompanyName)
        };

        return View(await PaginatedList<ProductionCompany>.CreateAsync(companies, pageNumber ?? 1, PageSize));
    }

    public async Task<IActionResult> Movies(long id, int? pageNumber)
    {
        var movies = _context.Movies
            .Include(m => m.MovieProductionCompanies)
            .Where(m => m.MovieProductionCompanies.Any(mpc => mpc.CompanyId == id))
            .OrderBy(m => m.Title);

        return View(await PaginatedList<Movie>.CreateAsync(movies, pageNumber ?? 1, PageSize));
    }

    public async Task<IActionResult> ManageKeywords(int id)
    {
        var movie = await _context.Movies
            .Include(m => m.MovieKeywords)
            .ThenInclude(mk => mk.Keyword)
            .FirstOrDefaultAsync(m => m.MovieId == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    [HttpPost]
    public async Task<IActionResult> AddKeyword(int MovieId, string Keyword)
    {
        if (!string.IsNullOrEmpty(Keyword))
        {
            var keywordEntity = await _context.Keywords
                .FirstOrDefaultAsync(k => k.KeywordName == Keyword);

            if (keywordEntity == null)
            {
                keywordEntity = new Keyword { KeywordName = Keyword };
                _context.Keywords.Add(keywordEntity);
                await _context.SaveChangesAsync();
            }

            var movie = await _context.Movies
                .Include(m => m.MovieKeywords)
                .FirstOrDefaultAsync(m => m.MovieId == MovieId);

            if (movie != null)
            {
                if (!movie.MovieKeywords.Any(mk => mk.KeywordId == keywordEntity.KeywordId))
                {
                    movie.MovieKeywords.Add(new MovieKeyword
                    {
                        MovieId = MovieId,
                        KeywordId = keywordEntity.KeywordId
                    });

                    await _context.SaveChangesAsync();
                }
            }
        }

        return RedirectToAction(nameof(ManageKeywords), new { id = MovieId });
    }
}