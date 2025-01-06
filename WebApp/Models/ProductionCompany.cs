namespace WebApp.Models;

public class ProductionCompany
{
    public int CompanyId { get; set; } // Odpowiada "company_id"
    public string CompanyName { get; set; } // Odpowiada "company_name"

    public ICollection<MovieProductionCompany> MovieProductionCompanies { get; set; }
}
