using ContributionSystem.ViewModels.Models.Contribution;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IContributionRepositoryService
    {
        public void AddContribution(RequestCalculateContributionViewModel request);
    }
}
