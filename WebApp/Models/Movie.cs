using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

[Table("movie")]
public class Movie
{
    [Column("movie_id")]
    public long MovieId { get; set; }
    
    [Column("title")]
    public string Title { get; set; }
    
    [Column("budget")]
    public long? Budget { get; set; }
    
    [Column("homepage")]
    public string? Homepage { get; set; }
    
    [Column("overview")]
    public string? Overview { get; set; }
    
    [Column("popularity")]
    public float? Popularity { get; set; }
    
    [Column("release_date")]
    public DateTime? ReleaseDate { get; set; }
    
    [Column("revenue")]
    public long? Revenue { get; set; }
    
    [Column("runtime")]
    public int? Runtime { get; set; }
    
    [Column("movie_status")]
    public string? MovieStatus { get; set; }
    
    [Column("tagline")]
    public string? Tagline { get; set; }
    
    [Column("vote_average")]
    public float? VoteAverage { get; set; }
    
    [Column("vote_count")]
    public int? VoteCount { get; set; }

    public ICollection<MovieKeyword> MovieKeywords { get; set; }
    public ICollection<MovieProductionCompany> MovieProductionCompanies { get; set; }
}