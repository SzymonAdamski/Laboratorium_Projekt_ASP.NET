namespace WebApp.Models;

public class MovieKeyword
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public int KeywordId { get; set; }
    public Keyword Keyword { get; set; }
    public ICollection<MovieKeyword> MovieKeywords { get; set; }

}
