using ContributionSystem.ViewModels.Models.Contribution;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IContributionService
    {
        public Task<ResponseCalculateContributionViewModel> Calculate(RequestCalculateContributionViewModel request);

        public Task<IEnumerable<ResponseGetRequestsHistoryContributionViewModel>> GetRequestsHistory(RequestGetRequestsHistoryContributionViewModel request);
    }
}