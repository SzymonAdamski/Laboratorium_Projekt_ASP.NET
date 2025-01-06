namespace WebApp.Models;

public class Keyword
{
    public int KeywordId { get; set; } // Odpowiada "keyword_id"
    public string KeywordName { get; set; } // Odpowiada "keyword_name"

    public ICollection<MovieKeyword> MovieKeywords { get; set; }
}
