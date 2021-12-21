using ContributionSystem.ViewModels.Common;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    /// <summary>
    /// Response model with calculation information per months.
    /// </summary>
    public class ResponseGetDetailsByIdContributionViewModel : CollectionOfItems<ResponseGetDetailsByIdContributionViewModelItem>
    {
        /// <summary>
        /// Contribution identifier.
        /// </summary>
        public int ContributionId { get; set; }
    }

    /// <summary>
    /// Calculation information per month.
    /// </summary>
    public class ResponseGetDetailsByIdContributionViewModelItem : MonthsInfoContributionViewModelItem
    {
        /// <summary>
        /// Month information identifier.
        /// </summary>
        public int Id { get; set; }
    }
}
