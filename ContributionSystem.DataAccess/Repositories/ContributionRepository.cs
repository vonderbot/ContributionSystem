using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContributionSystem.DataAccess.Repositories
{
    public class ContributionRepository : BaseRepository<Contribution>, IContributionRepository
    {
        public ContributionRepository(ContributionDbContext contributionDbContext)
            : base(contributionDbContext)
        {
        }

        public async Task<List<Contribution>> GetContributions(int take, int skip)
        {
            var contributions =  await _contributionDbContext.Contribution
                .OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return contributions;
        }
    }
}
