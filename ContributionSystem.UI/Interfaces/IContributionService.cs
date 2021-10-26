using ContributionSystem.ViewModels.Models.Contribution;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Interfaces
{
    public interface IContributionService
    {
        public Task<List<ResponseGetRequestsHistoryContributionViewModel>> GetRequestsHistory(int take, int skip);

        public Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request);
    }
}
