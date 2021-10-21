using ContributionSystem.ViewModels.Models.Contribution;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IContributionService
    {
        public ResponseCalculateContributionViewModel Calculate(RequestCalculateContributionViewModel request);
    }
}