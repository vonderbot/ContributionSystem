using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<List<Contribution>> Get(int take, int skip)
        {
            var contributions =  await _contributionDbContext.Contribution
                .OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return contributions;
        }

        public override async Task<Contribution> GetById(int id)
        {
            var contribution = await _contributionDbContext.Contribution.Include(c => c.Details).SingleOrDefaultAsync(c => c.Id == id);
            if (contribution == null)
            {
                throw new Exception("Can't find contribution");
            }

            return contribution;
        }
    }
}
