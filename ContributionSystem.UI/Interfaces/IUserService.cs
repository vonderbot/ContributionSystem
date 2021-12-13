using ContributionSystem.ViewModels.Models.User;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Interfaces
{
    public interface IUserService
    {
        public Task ChangeUserStatus(RequestChangeUserStatusUserViewModel request);

        public Task<ResponseGetUsersListUserViewModel> GetUsersList();
    }
}
