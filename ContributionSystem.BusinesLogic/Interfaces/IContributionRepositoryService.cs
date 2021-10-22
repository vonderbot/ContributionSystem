using ContributionSystem.Entities.Entities;
using ContributionSystem.ViewModels.Models.Contribution;
using System.Collections.Generic;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IContributionRepositoryService
    {
        public List<RequestCalculateContributionViewModel> GetRequestsHistory(RequestGetRequestHistoryContrbutionViewModel request);

        public void AddContribution(RequestCalculateContributionViewModel request, IEnumerable<ResponseCalculateContributionViewModelItem> responseItems);
    }
}