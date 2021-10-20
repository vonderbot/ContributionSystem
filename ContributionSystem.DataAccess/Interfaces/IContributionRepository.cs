using ContributionSystem.Entities.Entities;

namespace ContributionSystem.DataAccess.Interfaces
{
    public interface IContributionRepository
    {
        void Create(Contribution contribution);
    }
}
