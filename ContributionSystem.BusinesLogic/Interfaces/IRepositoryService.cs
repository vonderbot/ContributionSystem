using ContributionSystem.ViewModels.Models.Contribution;
using System.Collections.Generic;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IRepositoryService
    {
        public IEnumerable<RequestCalculateContributionViewModel> GetRequestsHistory(RequestGetRequestHistoryContrbutionViewModel request);

        public void AddContribution(RequestCalculateContributionViewModel request, IEnumerable<ResponseCalculateContributionViewModelItem> responseItems);
    }
}