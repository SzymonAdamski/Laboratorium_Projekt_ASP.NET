using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;


namespace WebApp.Controllers;

public class ProductionCompaniesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductionCompaniesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var companies = await _context.ProductionCompanies
            .Include(pc => pc.MovieProductionCompanies)
            .ThenInclude(mpc => mpc.Movie)
            .ToListAsync();

        return View(companies);
    }


    public async Task<IActionResult> Movies(int id)
    {
        var movies = await _context.MovieProductionCompanies
            .Where(mpc => mpc.CompanyId == id)
            .Select(mpc => mpc.Movie)
            .ToListAsync();

        return View(movies);
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

            var movieKeyword = new MovieKeyword
            {
                MovieId = MovieId,
                KeywordId = keywordEntity.KeywordId
            };

            _context.MovieKeywords.Add(movieKeyword);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(ManageKeywords), new { id = MovieId });
    }

}
