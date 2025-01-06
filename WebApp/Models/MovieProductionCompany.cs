using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

[Table("movie_company")]
public class MovieProductionCompany
{
    [Column("movie_id")]
    public long MovieId { get; set; }  // Zmiana na long
    public Movie Movie { get; set; }

    [Column("company_id")]
    public long CompanyId { get; set; }  // Zmiana na long
    public ProductionCompany ProductionCompany { get; set; }
}