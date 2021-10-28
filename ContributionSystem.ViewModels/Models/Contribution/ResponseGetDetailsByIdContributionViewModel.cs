using ContributionSystem.ViewModels.Items.Contribution;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class ResponseGetDetailsByIdContributionViewModel : CollectionOfItems<MonthsInfoContributionViewModelItem>
    {
        public int ContributionId { get; set; }
    }
}
