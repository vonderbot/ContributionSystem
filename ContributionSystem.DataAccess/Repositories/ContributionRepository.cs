﻿using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ContributionSystem.DataAccess.Repositories
{
    public class ContributionRepository : BaseRepository, IContributionRepository
    {
        public ContributionRepository(ContributionDbContext newContributionDbContext)
            :base(newContributionDbContext)
        {
        }

        //public IEnumerable<Contribution> GetContributionList()
        //{
        //    return _contributionDbContext.Contribution.ToList();
        //}

        public void Create(Contribution newContribution)
        {
            _contributionDbContext.Contribution.Add(newContribution);
            SaveChanges();
        }
    }
}
