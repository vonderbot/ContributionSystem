using ContributionSystem.ViewModels.Models.Contribution;
using System.Collections.Generic;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IContributionRepositoryService
    {
        public void AddContribution(RequestCalculateContributionViewModel request, IEnumerable<ResponseCalculateContributionViewModelItem> responseItems);
    }
}