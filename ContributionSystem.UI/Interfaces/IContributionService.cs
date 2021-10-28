using ContributionSystem.ViewModels.Models.Contribution;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Interfaces
{
    public interface IContributionService
    {
        public Task<ResponseGetDetailsByIdContributionViewModel> GetDetailsById(int id);

        public Task<ResponseGetHistoryContributionViewModel> GetHistory(int take, int skip);

        public Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request);
    }
}
