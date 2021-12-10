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
        private readonly GraphServiceClient _graphClient;

        public UserService(GraphServiceClient graphClient)
        {
            _graphClient = graphClient;
        }

        public async Task ChangeUserStatus(RequestChangeUserStatusUserViewModel request)
        {
            CheckChangeUserStatusRequest(request);
            var user = new User
            {
                AccountEnabled = request.AccountEnabled
            };
            await _graphClient.Users[request.Id].Request().UpdateAsync(user);
        }

        public async Task<ResponseGetUsersListUserViewModel> GetUsersList()
        {
            var users = await _graphClient.Users.Request().Select("Id,DisplayName,Mail,AccountEnabled").GetAsync();
            var response = new ResponseGetUsersListUserViewModel()
            {
                Items = users.Select(u => new ResponseGetUsersListContributionViewModelItem
                {
                    Id = u.Id,
                    Name = u.DisplayName,
                    Email = u.Mail,
                    Status = u.AccountEnabled.GetValueOrDefault()
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

        private void CheckChangeUserStatusRequest(RequestChangeUserStatusUserViewModel request)
        {
            if (request == null)
            {
                throw new Exception("Null request");
            }
            else if (String.IsNullOrEmpty(request.Id))
            {
                throw new Exception("User id can`t be null or empty");
            }
        }
    }
}
