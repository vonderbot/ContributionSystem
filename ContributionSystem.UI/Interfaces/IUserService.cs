using ContributionSystem.ViewModels.Models.User;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Interfaces
{
    /// <summary>
    /// Provides methods to communicate with api.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Сhanges user status.
        /// </summary>
        /// <param name="request">Request model with user info.</param>
        /// <returns>Task</returns>
        public Task ChangeUserStatus(RequestChangeUserStatusUserViewModel request);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>Response model with list of users.</returns>
        public Task<ResponseGetUsersListUserViewModel> GetUsersList();
    }
}
