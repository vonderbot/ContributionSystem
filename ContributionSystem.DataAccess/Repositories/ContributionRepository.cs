using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;

namespace ContributionSystem.DataAccess.Repositories
{
    public class ContributionRepository : BaseRepository, IContributionRepository
    {
        public ContributionRepository(ContributionDbContext contributionDbContext)
            :base(contributionDbContext)
        {
        }

        public void Create(Contribution contribution)
        {
            _contributionDbContext.Contribution.Add(contribution);
            SaveChanges();
        }
    }
}
