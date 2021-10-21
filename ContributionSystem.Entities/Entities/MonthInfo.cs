namespace ContributionSystem.Entities.Entities
{
    public class MonthInfo
    {
        public int Id { get; set; }

        public int MonthNumber { get; set; }

        public decimal Income { get; set; }

        public decimal Sum { get; set; }

        public int ContributionId { get; set; }

        public Contribution Contribution { get; set; }
    }
}
