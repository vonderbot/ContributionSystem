using ContributionSystem.ViewModels.Models.Contribution;
using System.Collections.Generic;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IContributionService
    {
        public ResponseCalculateContributionViewModel Calculate(RequestCalculateContributionViewModel request);

        public IEnumerable<RequestCalculateContributionViewModel> GetRequestsHistory(RequestGetRequestsHistoryContrbutionViewModel request);

        public void AddContribution(RequestCalculateContributionViewModel request, IEnumerable<ResponseCalculateContributionViewModelItem> responseItems);
    }
}