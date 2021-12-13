using ContributionSystem.ViewModels.Common;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    /// <summary>
    /// Response model with calculation info per months.
    /// </summary>
    public class ResponseGetDetailsByIdContributionViewModel : CollectionOfItems<ResponseGetDetailsByIdContributionViewModelItem>
    {
        /// <summary>
        /// Contribution id
        /// </summary>
        public int ContributionId { get; set; }
    }

    /// <summary>
    /// Calculation info per month.
    /// </summary>
    public class ResponseGetDetailsByIdContributionViewModelItem : MonthsInfoContributionViewModelItem
    {
        /// <summary>
        /// Month info id.
        /// </summary>
        public int Id { get; set; }
    }
}
