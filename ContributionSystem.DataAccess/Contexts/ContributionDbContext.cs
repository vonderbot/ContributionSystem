using ContributionSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContributionSystem.DataAccess.Contexts
{
    /// <summary>
    /// Contribution db.
    /// </summary>
    public sealed class ContributionDbContext : DbContext
    {
        /// <summary>
        /// Contribution general info.
        /// </summary>
        public DbSet<Contribution> Contribution { get; set; }

        /// <summary>
        /// Contribution info per month.
        /// </summary>
        public DbSet<MonthInfo> MonthInfo { get; set; }

        /// <summary>
        /// ContributionDbContext constructor.
        /// </summary>
        /// <param name="options">DbContextOptions instance.</param>
        public ContributionDbContext(DbContextOptions<ContributionDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
