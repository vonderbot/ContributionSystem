using ContributionSystem.ViewModels.Models.Contribution;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        public string GetUserId(ClaimsPrincipal user);

        public Task<ResponseGetUsersListContributionViewModel> GetUsersList(string accessToken);
    }
}
