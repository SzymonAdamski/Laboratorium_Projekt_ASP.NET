namespace WebApp.Models;

public class MovieProductionCompany
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public int CompanyId { get; set; }
    public ProductionCompany ProductionCompany { get; set; }
}
