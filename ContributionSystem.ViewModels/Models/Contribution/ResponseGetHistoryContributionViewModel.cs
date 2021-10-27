using System.Collections.Generic;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class ResponseGetHistoryContributionViewModel
    {
        public IEnumerable<ResponseGetHistoryContributionViewModelItem> Items { get; set; }

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
