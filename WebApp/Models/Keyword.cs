using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

[Table("keyword")]
public class Keyword
{
    [Column("keyword_id")]
    public long KeywordId { get; set; }
    
    [Column("keyword_name")]
    public string KeywordName { get; set; }

    public ICollection<MovieKeyword> MovieKeywords { get; set; }
}