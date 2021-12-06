using ContributionSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContributionSystem.DataAccess.Contexts
{
    public sealed class ContributionDbContext : DbContext
    {
        public DbSet<Contribution> Contribution { get; set; }

        public DbSet<MonthInfo> MonthInfo { get; set; }

        public ContributionDbContext(DbContextOptions<ContributionDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
