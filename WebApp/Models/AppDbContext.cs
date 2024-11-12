using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class AppDbContext : DbContext
{
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }

    private string DbPath { get; set; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Combine(path, "contacts.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactEntity>()
            .HasOne<OrganizationEntity>(c => c.Organization)
            .WithMany(o => o.Contacts)
            .HasForeignKey(c => c.OrganizationId);

        modelBuilder.Entity<OrganizationEntity>()
            .ToTable("organizations")
            .HasData(
                new OrganizationEntity
                {
                    Id = 101,
                    Name = "WSEI",
                    NIP = "283792834",
                    REGON = "2837294234"
                },
                new OrganizationEntity
                {
                    Id = 102,
                    Name = "PKP",
                    NIP = "283792834",
                    REGON = "2837294234"
                }
            );

        modelBuilder.Entity<OrganizationEntity>()
            .OwnsOne(organization => organization.Address)
            .HasData(
                new { OrganizationEntityId = 101, City = "Kraków", Street = "św. Filipa 17" },
                new { OrganizationEntityId = 102, City = "Warszawa", Street = "Dworcowa 9" }
            );

        modelBuilder.Entity<ContactEntity>()
            .HasData(
                new ContactEntity
                {
                    Id = 1,
                    FirstName = "Adam",
                    LastName = "Kowal",
                    Email = "adam@wsei.edu.pl",
                    PhoneNumber = "123456789",
                    BirthDate = new DateOnly(2000, 10, 10),
                    Created = DateTime.Now,
                    OrganizationId = 101
                },
                new ContactEntity
                {
                    Id = 2,
                    FirstName = "Ewa",
                    LastName = "Kowal",
                    Email = "ewa@wsei.edu.pl",
                    PhoneNumber = "123456789",
                    BirthDate = new DateOnly(2000, 10, 10),
                    Created = DateTime.Now,
                    OrganizationId = 102
                }
            );
    }
}