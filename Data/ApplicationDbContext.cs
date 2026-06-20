using AKAnkietyProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AKAnkietyProject.Data
{
    // Kontekst bazy danych
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}
