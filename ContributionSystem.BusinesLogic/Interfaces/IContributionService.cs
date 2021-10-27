using ContributionSystem.ViewModels.Models.Contribution;
using System.Threading.Tasks;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IContributionService
    {
        public Task<ResponseCalculateContributionViewModel> Calculate(RequestCalculateContributionViewModel request);

        public Task<ResponseGetHistoryContributionViewModel> GetHistory(RequestGetRequestsHistoryContributionViewModel request);
    }
}