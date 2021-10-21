using ContributionSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ContributionSystem.DataAccess.Contexts
{
    public class ContributionDbContext : DbContext
    {
        public DbSet<Contribution> Contribution { get; set; }

        public DbSet<MonthInfo> MonthInfo { get; set; }

        public ContributionDbContext(DbContextOptions<ContributionDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }

    public class ContributionDbContextFactory : IDesignTimeDbContextFactory<ContributionDbContext>
    {
        public ContributionDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContributionDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ContributionDb; Trusted_Connection=True;");

            return new ContributionDbContext(optionsBuilder.Options);
        }
    }
}
