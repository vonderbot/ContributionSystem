using ContributionSystem.ViewModels.Common;

namespace ContributionSystem.ViewModels.Models.User
{
    /// <summary>
    /// Response with list of users information.
    /// </summary>
    public class ResponseGetUsersListUserViewModel : CollectionOfItems<ResponseGetUsersListContributionViewModelItem>
    {
    }

    /// <summary>
    /// User information.
    /// </summary>
    public class ResponseGetUsersListContributionViewModelItem
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User status.
        /// </summary>
        public bool Status { get; set; }
    }
}
