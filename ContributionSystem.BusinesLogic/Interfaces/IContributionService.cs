using ContributionSystem.ViewModels.Models.Contribution;
using System.Threading.Tasks;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IContributionService
    {
        Task<ResponseGetDetailsContributionViewModel> GetDetails(int id);

        public Task<ResponseCalculateContributionViewModel> Calculate(RequestCalculateContributionViewModel request);

        public Task<ResponseGetHistoryContributionViewModel> GetHistory(RequestGetHistoryContributionViewModel request);
    }
}