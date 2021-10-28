using ContributionSystem.ViewModels.Common;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class ResponseGetDetailsByIdContributionViewModel : CollectionOfItems<ResponseGetDetailsByIdContributionViewModelItem>
    {
        public int ContributionId { get; set; }
    }

    public class ResponseGetDetailsByIdContributionViewModelItem : MonthsInfoContributionViewModelItem
    {
        public int Id { get; set; }
    }
}
