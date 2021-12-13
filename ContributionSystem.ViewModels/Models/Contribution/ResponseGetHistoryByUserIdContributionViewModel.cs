using ContributionSystem.ViewModels.Common;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    /// <summary>
    /// Response model with info to get a part of records.
    /// </summary>
    public class ResponseGetHistoryByUserIdContributionViewModel : CollectionOfItems<ResponseGetUsersListContributionViewModelItems>
    {
        /// <summary>
        /// Total number of user records.
        /// </summary>
        public int TotalNumberOfUserRecords { get; set; }

        /// <summary>
        /// Number of records to take.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Number of records to skip.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// User id.
        /// </summary>
        public string UserId { get; set; }
    }
    
    /// <summary>
    /// User calculation general info.
    /// </summary>
    public class ResponseGetUsersListContributionViewModelItems
    {
        /// <summary>
        /// Percent per year.
        /// </summary>
        public decimal Percent { get; set; }

        /// <summary>
        /// Term in months.
        /// </summary>
        public int Term { get; set; }

        /// <summary>
        /// Start value.
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Date of creating calculation.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Contribution id.
        /// </summary>
        public int Id { get; set; }
    }
}
