using ContributionSystem.ViewModels.Models.User;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        public Task ChangeUserStatus(RequestChangeUserStatusUserViewModel request);

        public string GetUserId(ClaimsPrincipal user);

        public Task<ResponseGetUsersListUserViewModel> GetUsersList();
    }
}
