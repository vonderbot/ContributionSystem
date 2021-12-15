using ContributionSystem.ViewModels.Models.User;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    /// <summary>
    /// Provides methods for interacting with users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Сhanges user status.
        /// </summary>
        /// <param name="request">Request model with user information.</param>
        /// <returns><see cref="Task" /></returns>
        public Task ChangeUserStatus(RequestChangeUserStatusUserViewModel request);

        /// <summary>
        /// Gets id from user.
        /// </summary>
        /// <param name="user">User with claims.</param>
        /// <returns>User id as a string.</returns>
        public string GetUserId(ClaimsPrincipal user);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>Response model with list of users.</returns>
        public Task<ResponseGetUsersListUserViewModel> GetUsersList();
    }
}
