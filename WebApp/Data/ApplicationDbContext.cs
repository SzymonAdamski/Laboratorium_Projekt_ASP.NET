using Microsoft.EntityFrameworkCore;
using WebApp.Models;
namespace WebApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Keyword> Keywords { get; set; }
    public DbSet<ProductionCompany> ProductionCompanies { get; set; }
    public DbSet<MovieKeyword> MovieKeywords { get; set; }
    public DbSet<MovieProductionCompany> MovieProductionCompanies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Tabela movie
        modelBuilder.Entity<Movie>().ToTable("movie");
        modelBuilder.Entity<Movie>().HasKey(m => m.MovieId);

        // Tabela keyword
        modelBuilder.Entity<Keyword>().ToTable("keyword");
        modelBuilder.Entity<Keyword>().HasKey(k => k.KeywordId);

        // Tabela production_company
        modelBuilder.Entity<ProductionCompany>().ToTable("production_company");
        modelBuilder.Entity<ProductionCompany>().HasKey(pc => pc.CompanyId);

        // Tabela pośrednia: MovieKeyword
        modelBuilder.Entity<MovieKeyword>().HasKey(mk => new { mk.MovieId, mk.KeywordId });
        modelBuilder.Entity<MovieKeyword>().HasOne(mk => mk.Movie).WithMany(m => m.MovieKeywords).HasForeignKey(mk => mk.MovieId);
        modelBuilder.Entity<MovieKeyword>().HasOne(mk => mk.Keyword).WithMany(k => k.MovieKeywords).HasForeignKey(mk => mk.KeywordId);

        // Tabela pośrednia: MovieProductionCompany
        modelBuilder.Entity<MovieProductionCompany>().HasKey(mpc => new { mpc.MovieId, mpc.CompanyId });
        modelBuilder.Entity<MovieProductionCompany>().HasOne(mpc => mpc.Movie).WithMany(m => m.MovieProductionCompanies).HasForeignKey(mpc => mpc.MovieId);
        modelBuilder.Entity<MovieProductionCompany>().HasOne(mpc => mpc.ProductionCompany).WithMany(pc => pc.MovieProductionCompanies).HasForeignKey(mpc => mpc.CompanyId);
    }
}