using ContributionSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContributionSystem.DataAccess
{
    class ContributionDbContext : DbContext
    {
        public DbSet<Contribution> Contribution { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ContributionDB; Trusted_Connection=True;");
        }

        //public  void AddContribution(Contribution newContribution)
        //{
        //    this.Contribution.Add(newContribution);
        //    this.SaveChanges();
        //}
    }
}
