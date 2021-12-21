namespace ContributionSystem.ViewModels.Models.Contribution
{
    /// <summary>
    /// Request model with information to get a part of records.
    /// </summary>
    public class RequestGetHistoryByUserIdContributionViewModel
    {
        /// <summary>
        /// Number of records to take.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Number of records to skip.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }
    }
}
