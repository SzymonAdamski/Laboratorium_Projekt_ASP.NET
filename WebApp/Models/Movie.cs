namespace WebApp.Models;

public class Movie
{
    public int MovieId { get; set; } // Odpowiada "movie_id"
    public string Title { get; set; } // Odpowiada "title"
    public int Budget { get; set; } // Odpowiada "budget"
    public string Homepage { get; set; } // Odpowiada "homepage"
    public string Overview { get; set; } // Odpowiada "overview"
    public float Popularity { get; set; } // Odpowiada "popularity"
    public DateTime ReleaseDate { get; set; } // Odpowiada "release_date"
    public int Revenue { get; set; } // Odpowiada "revenue"
    public int Runtime { get; set; } // Odpowiada "runtime"
    public string MovieStatus { get; set; } // Odpowiada "movie_status"
    public string Tagline { get; set; } // Odpowiada "tagline"
    public float VoteAverage { get; set; } // Odpowiada "vote_average"
    public int VoteCount { get; set; } // Odpowiada "vote_count"

    public ICollection<MovieKeyword> MovieKeywords { get; set; }
    public ICollection<MovieProductionCompany> MovieProductionCompanies { get; set; }
}