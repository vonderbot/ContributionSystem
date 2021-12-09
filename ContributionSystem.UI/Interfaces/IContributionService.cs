using ContributionSystem.ViewModels.Models.Contribution;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Interfaces
{
    public interface IContributionService
    {
        public Task ChangeUserStatus(RequestChangeUserStatusContributionViewModel request);
        public Task<ResponseGetUsersListContributionViewModel> GetUsersList();

        public Task<ResponseGetDetailsByIdContributionViewModel> GetDetailsById(int id);

        public Task<ResponseGetHistoryByUserIdContributionViewModel> GetHistoryByUserId(int take, int skip);

        public Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request);
    }
}
