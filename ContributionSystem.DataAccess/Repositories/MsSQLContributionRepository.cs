using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;

namespace ContributionSystem.DataAccess.Repositories
{
    public class MsSQLContributionRepository : IContributionRepository
    {
        private readonly ContributionDbContext _contributionDbContext;

        public MsSQLContributionRepository(ContributionDbContext newContributionDbContext)
        {
            _contributionDbContext = newContributionDbContext;
        }

        public void Create(Contribution newContribution)
        {
            _contributionDbContext.Contribution.Add(newContribution);
            _contributionDbContext.SaveChanges();
        }
    }
}
