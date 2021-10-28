using ContributionSystem.ViewModels.Common;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class ResponseGetHistoryContributionViewModel : CollectionOfItems<ResponseGetHistoryContributionViewModelItem>
    {
        public int TotalNumberOfRecords { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }
    }

    public class ResponseGetHistoryContributionViewModelItem
    {
        public decimal Percent { get; set; }

        public int Term { get; set; }

        public decimal Sum { get; set; }

        public string Date { get; set; }

        public int Id { get; set; }
    }
}
