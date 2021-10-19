using ContributionSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContributionSystem.DataAccess
{
    class Class1
    {
    }

    class AppContext : DbContext
    {
        public DbSet<Contribution> Contribution { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=ContributionDB; Trusted_Connection=True");
        }
    }
}
