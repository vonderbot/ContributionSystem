using System.ComponentModel.DataAnnotations.Schema;

namespace ContributionSystem.Entities.Entities
{
    public class MonthInfo : BaseEntity
    {
        public int MonthNumber { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Income { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Sum { get; set; }

        [ForeignKey("Contribution")]
        public int ContributionId { get; set; }

        public Contribution Contribution { get; set; }
    }
}
