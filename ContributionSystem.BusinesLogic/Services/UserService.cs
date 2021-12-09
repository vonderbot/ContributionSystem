using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.Graph;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContributionSystem.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private GraphServiceClient _graphClient;

        public UserService(GraphServiceClient graphClient) 
        {
            _graphClient = graphClient;
        }

        public async Task ChangeUserStatus(RequestChangeUserStatusContributionViewModel request)
        {
            var user = new User
            {
                AccountEnabled = request.NewStatus
            };
            await _graphClient.Users[request.Id].Request().UpdateAsync(user);
        }

        public async Task<ResponseGetUsersListContributionViewModel> GetUsersList()
        {
            var users = await _graphClient.Users.Request().GetAsync();
            var response = new ResponseGetUsersListContributionViewModel()
            {
                Items = users.Select(u => new ResponseGetUsersListContributionViewModelItem
                {
                    Id = u.Id,
                    Name = u.DisplayName,
                    Email = u.Mail,
                    Status = _graphClient.Users[u.Id].Request().Select(c => c.AccountEnabled).GetAsync().Result.AccountEnabled.GetValueOrDefault()
                }).ToList()
            };

            return response;
        }

        public string GetUserId(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User have no id");
            }
            else
            {
                return userId;
            }
        }
    }
}
