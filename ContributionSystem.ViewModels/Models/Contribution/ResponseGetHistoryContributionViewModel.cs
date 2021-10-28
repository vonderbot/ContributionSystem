using ContributionSystem.ViewModels.Items.Contribution;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class ResponseGetHistoryContributionViewModel : CollectionOfItems<ResponseGetHistoryContributionViewModelItem>
    {
        public int TotalNumberOfRecords { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }
    }
}
