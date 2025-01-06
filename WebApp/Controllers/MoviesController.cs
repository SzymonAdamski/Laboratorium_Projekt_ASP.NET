using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers;

public class MoviesController : Controller
{
    private readonly ApplicationDbContext _context;
    private const int PageSize = 10;

    public MoviesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string searchString)
    {
        ViewData["CurrentSort"] = sortOrder;
        ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        ViewData["DateSortParm"] = sortOrder == "date" ? "date_desc" : "date";
        ViewData["CurrentFilter"] = searchString;

        var movies = _context.Movies
            .Include(m => m.MovieProductionCompanies)
            .ThenInclude(mpc => mpc.ProductionCompany)
            .Include(m => m.MovieKeywords)
            .ThenInclude(mk => mk.Keyword)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            movies = movies.Where(m => m.Title.Contains(searchString));
        }

        movies = sortOrder switch
        {
            "title_desc" => movies.OrderByDescending(m => m.Title),
            "date" => movies.OrderBy(m => m.ReleaseDate),
            "date_desc" => movies.OrderByDescending(m => m.ReleaseDate),
            _ => movies.OrderBy(m => m.Title)
        };

        return View(await PaginatedList<Movie>.CreateAsync(movies, pageNumber ?? 1, PageSize));
    }

    public async Task<IActionResult> Details(long id)
    {
        var movie = await _context.Movies
            .Include(m => m.MovieProductionCompanies)
            .ThenInclude(mpc => mpc.ProductionCompany)
            .Include(m => m.MovieKeywords)
            .ThenInclude(mk => mk.Keyword)
            .FirstOrDefaultAsync(m => m.MovieId == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    public async Task<IActionResult> ManageKeywords(long id)
    {
        var movie = await _context.Movies
            .Include(m => m.MovieKeywords)
            .ThenInclude(mk => mk.Keyword)
            .Include(m => m.MovieProductionCompanies)
            .FirstOrDefaultAsync(m => m.MovieId == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    [HttpPost]
    public async Task<IActionResult> AddKeyword(long movieId, string keyword)
    {
        if (string.IsNullOrEmpty(keyword))
        {
            return BadRequest("Słowo kluczowe nie może być puste");
        }

        var movie = await _context.Movies
            .Include(m => m.MovieKeywords)
            .ThenInclude(mk => mk.Keyword)
            .FirstOrDefaultAsync(m => m.MovieId == movieId);

        if (movie == null)
        {
            return NotFound("Film nie został znaleziony");
        }

        // Sprawdź, czy słowo kluczowe już istnieje
        var existingKeyword = await _context.Keywords
            .FirstOrDefaultAsync(k => k.KeywordName.ToLower() == keyword.ToLower());

        if (existingKeyword == null)
        {
            // Jeśli nie istnieje, utwórz nowe
            existingKeyword = new Keyword { KeywordName = keyword };
            _context.Keywords.Add(existingKeyword);
            await _context.SaveChangesAsync();
        }

        // Sprawdź, czy film już ma to słowo kluczowe
        if (!movie.MovieKeywords.Any(mk => mk.KeywordId == existingKeyword.KeywordId))
        {
            movie.MovieKeywords.Add(new MovieKeyword
            {
                MovieId = movie.MovieId,
                KeywordId = existingKeyword.KeywordId
            });
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(ManageKeywords), new { id = movieId });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveKeyword(long movieId, long keywordId)
    {
        var movieKeyword = await _context.MovieKeywords
            .FirstOrDefaultAsync(mk => mk.MovieId == movieId && mk.KeywordId == keywordId);

        if (movieKeyword != null)
        {
            _context.MovieKeywords.Remove(movieKeyword);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(ManageKeywords), new { id = movieId });
    }
}