using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace WebApp.Models;

public class AppDbContext : IdentityDbContext<IdentityUser>
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
        base.OnModelCreating(modelBuilder);
        
        string USER_ID = Guid.NewGuid().ToString();
        string ADMIN_ID = Guid.NewGuid().ToString();
        string USER_RULE_ID = Guid.NewGuid().ToString();
        string ADMIN_RULE_ID = Guid.NewGuid().ToString();

        modelBuilder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole()
                {
                    Id = USER_RULE_ID,
                    Name = "user",
                    NormalizedName = "USER",
                    ConcurrencyStamp = USER_RULE_ID
                },
                new IdentityRole()
                {
                    Id = ADMIN_RULE_ID,
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = ADMIN_RULE_ID
                }
            );
        var user = new IdentityUser()
        {
            Id = USER_ID,
            Email = "adamek@wp.pl",
            NormalizedEmail = "ADAMEK@WP.PL",
            UserName = "Adamek",
            NormalizedUserName = "ADAMEK",
            EmailConfirmed = true,
            
        };
        var admin = new IdentityUser()
        {
            Id = ADMIN_ID,
            Email = "adamek@wp.pl",
            NormalizedEmail = "ADAMEK@WP.PL",
            UserName = "Damian",
            NormalizedUserName = "DAMIAN",
            EmailConfirmed = true,
            
        };
        PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
        hasher.HashPassword(user, "1234!");
        hasher.HashPassword(admin, "1234!");
        modelBuilder.Entity<IdentityUser>()
            .HasData(user, admin);
        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = ADMIN_RULE_ID,
                    UserId = ADMIN_ID
                },
                new IdentityUserRole<string>()
                {
                    RoleId = USER_RULE_ID,
                    UserId = ADMIN_ID,
                },
                new IdentityUserRole<string>()
                {
                    RoleId = USER_RULE_ID,
                    UserId = USER_ID,
                }
            );
        
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