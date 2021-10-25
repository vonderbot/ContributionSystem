using ContributionSystem.ViewModels.Models.Contribution;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IContributionService
    {
        public ResponseCalculateContributionViewModel Calculate(RequestCalculateContributionViewModel request);

        public Task<IEnumerable<RequestCalculateContributionViewModel>> GetRequestsHistory(RequestGetRequestsHistoryContrbutionViewModel request);

        public Task AddContribution(RequestCalculateContributionViewModel request, IEnumerable<ResponseCalculateContributionViewModelItem> responseItems);
    }
}