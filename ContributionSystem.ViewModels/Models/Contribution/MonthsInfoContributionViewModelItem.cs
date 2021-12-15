namespace ContributionSystem.ViewModels.Models.Contribution
{
    /// <summary>
    /// Calculation information per month.
    /// </summary>
    public class MonthsInfoContributionViewModelItem
    {
        /// <summary>
        /// The ordinal number of the month.
        /// </summary>
        public int MonthNumber { get; set; }

        /// <summary>
        /// Monthly income.
        /// </summary>
        public decimal Income { get; set; }

        /// <summary>
        /// Sum on contribution.
        /// </summary>
        public decimal Sum { get; set; }
    }
}
