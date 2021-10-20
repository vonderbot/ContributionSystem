using ContributionSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContributionSystem.DataAccess.Contexts
{
    public class ContributionDbContext : DbContext
    {
        public DbSet<Contribution> Contribution { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ContributionDB; Trusted_Connection=True;");
        }
    }
}
