using ContributionSystem.ViewModels.Items.Contribution;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class ResponseGetDetailsContributionViewModel : CollectionOfItems<MonthsInfoContributionViewModelItem>
    {
        public int ContributionId { get; set; }
    }
}
