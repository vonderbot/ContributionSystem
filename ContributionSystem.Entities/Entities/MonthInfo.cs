using System.ComponentModel.DataAnnotations.Schema;

namespace ContributionSystem.Entities.Entities
{
    public class MonthInfo
    {
        public int Id { get; set; }

        public int MonthNumber { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Income { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Sum { get; set; }

        public int ContributionId { get; set; }

        public Contribution Contribution { get; set; }
    }
}
