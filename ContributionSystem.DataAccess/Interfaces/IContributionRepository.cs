using ContributionSystem.Entities.Entities;
using System.Collections.Generic;

namespace ContributionSystem.DataAccess.Interfaces
{
    public interface IContributionRepository : IBaseRepository<Contribution>
    {
        List<Contribution> GetContributions(int numberOfContrbutionForLoad, int numberOfContrbutionForSkip);
    }
}
