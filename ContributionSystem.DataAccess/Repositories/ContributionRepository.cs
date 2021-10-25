using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ContributionSystem.DataAccess.Repositories
{
    public class ContributionRepository : BaseRepository<Contribution>, IContributionRepository
    {
        public ContributionRepository(ContributionDbContext contributionDbContext)
            : base(contributionDbContext)
        {
        }

        public List<Contribution> GetContributions(int numberOfContrbutionForLoad, int numberOfContrbutionForSkip)
        {
            var contributions = _contributionDbContext.Contribution
                .OrderByDescending(x => x.Id)
                .Skip(numberOfContrbutionForSkip)
                .Take(numberOfContrbutionForLoad)
                .ToList();

            return contributions;
        }
    }
}
