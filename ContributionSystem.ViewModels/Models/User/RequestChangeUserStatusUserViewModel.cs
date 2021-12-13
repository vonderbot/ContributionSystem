namespace ContributionSystem.ViewModels.Models.User
{
    /// <summary>
    /// Request with information to change user status.
    /// </summary>
    public class RequestChangeUserStatusUserViewModel
    {
        /// <summary>
        /// New status for user.
        /// </summary>
        public bool AccountEnabled { get; set; }

        /// <summary>
        /// User id.
        /// </summary>
        public string Id { get; set; }
    }
}
