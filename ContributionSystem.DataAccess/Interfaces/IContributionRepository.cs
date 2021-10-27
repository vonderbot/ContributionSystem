﻿using ContributionSystem.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.DataAccess.Interfaces
{
    public interface IContributionRepository : IBaseRepository<Contribution>
    {
        Task<List<Contribution>> GetContributions(int take, int skip);

        Task<Contribution> GetContributionById(int id);
    }
}
