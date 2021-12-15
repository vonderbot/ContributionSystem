using ContributionSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContributionSystem.DataAccess.Contexts
{
    /// <summary>
    /// Contribution database context.
    /// </summary>
    public sealed class ContributionDbContext : DbContext
    {
        /// <summary>
        /// Contribution data set.
        /// </summary>
        public DbSet<Contribution> Contribution { get; set; }

        /// <summary>
        /// Contribution calculation data set per month.
        /// </summary>
        public DbSet<MonthInfo> MonthInfo { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="ContributionDbContext" />.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions" /> instance.</param>
        public ContributionDbContext(DbContextOptions<ContributionDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
