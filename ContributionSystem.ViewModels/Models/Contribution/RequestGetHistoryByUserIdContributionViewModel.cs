namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class RequestGetHistoryByUserIdContributionViewModel
    {
        public int Take { get; set; }

        public int Skip { get; set; }

        public string UserId { get; set; }
    }
}
