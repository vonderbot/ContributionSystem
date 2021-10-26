using ContributionSystem.ViewModels.Models.Contribution;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IContributionService
    {
        public ResponseCalculateContributionViewModel Calculate(RequestCalculateContributionViewModel request);

        public Task<IEnumerable<ResponseGetRequestsHistoryContributionViewModel>> GetRequestsHistory(RequestGetRequestsHistoryContributionViewModel request);

        public Task AddContribution(RequestCalculateContributionViewModel request, IEnumerable<ResponseCalculateContributionViewModelItem> responseItems);
    }
}