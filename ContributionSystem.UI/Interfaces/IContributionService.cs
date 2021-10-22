using ContributionSystem.ViewModels.Models.Contribution;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Interfaces
{
    public interface IContributionService
    {
        public Task<List<RequestCalculateContributionViewModel>> GetRequestsHistory(int numberOfContrbutionForLoad, int numberOfContrbutionForSkip);

        public Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request);
    }
}
