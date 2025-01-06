using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

[Table("movie_keywords")]
public class MovieKeyword
{
    [Column("movie_id")]
    public long MovieId { get; set; }
    public Movie Movie { get; set; }

    [Column("keyword_id")]
    public long KeywordId { get; set; }
    public Keyword Keyword { get; set; }
}