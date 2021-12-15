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
    /// <inheritdoc/>
    public class ContributionRepository : BaseRepository<Contribution>, IContributionRepository
    {
        /// <summary>
        /// Creates a new instance of <see cref="ContributionRepository" />.
        /// </summary>
        /// <param name="contributionDbContext"><see cref="ContributionDbContext" /> instance.</param>
        public ContributionRepository(ContributionDbContext contributionDbContext)
            : base(contributionDbContext)
        {
        }

        /// <inheritdoc/>
        public async Task<int> GetNumberOfUserRecords(string userId)
        {
            return await Table.Where(c => c.UserId == userId).CountAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Contribution>> Get(int take, int skip)
        {
            var contributions =  await ContributionDbContext.Contribution
                .OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return contributions;
        }

        /// <inheritdoc/>
        public async Task<List<Contribution>> GetByUserId(int take, int skip, string userId)
        {
            var contributions = await ContributionDbContext.Contribution
                .Include(c => c.Details)
                .Where(c => c.UserId == userId)
                .OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return contributions;
        }

        /// <inheritdoc/>
        public override async Task<Contribution> GetById(int id)
        {
            var contribution = await ContributionDbContext.Contribution.Include(c => c.Details).SingleOrDefaultAsync(c => c.Id == id);
            if (contribution == null)
            {
                throw new Exception("Can't find contribution");
            }

            return contribution;
        }
    }
}
