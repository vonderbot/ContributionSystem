using ContributionSystem.Entities.Entities;
using System.Collections.Generic;

namespace ContributionSystem.DataAccess.Interfaces
{
    public interface IContributionRepository
    {
        List<Contribution> GetContributions(int numberOfContrbutionForLoad, int numberOfContrbutionForSkip);
        void Create(Contribution contribution);
    }
}
