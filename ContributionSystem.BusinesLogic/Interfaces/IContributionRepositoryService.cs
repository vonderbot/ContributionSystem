using ContributionSystem.Entities.Entities;
using ContributionSystem.ViewModels.Models.Contribution;
using System.Collections.Generic;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IContributionRepositoryService
    {
        public void AddContribution(RequestCalculateContributionViewModel request, IEnumerable<ResponseCalculateContributionViewModelItem> details);

        //public IEnumerable<Contribution> GetContributionList();
    }
}
