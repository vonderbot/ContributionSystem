using ContributionSystem.Entities.Entities;
using System.Collections.Generic;

namespace ContributionSystem.DataAccess.Interfaces
{
    public interface IContributionRepository
    {
        //IEnumerable<Contribution> GetContributionList();

        void Create(Contribution contribution);
    }
}
