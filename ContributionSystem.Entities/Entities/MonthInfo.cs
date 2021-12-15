using System.ComponentModel.DataAnnotations.Schema;

namespace ContributionSystem.Entities.Entities
{
    /// <summary>
    /// Calculation information per month.
    /// </summary>
    public class MonthInfo : BaseEntity
    {
        /// <summary>
        /// The ordinal number of the month.
        /// </summary>
        public int MonthNumber { get; set; }

        /// <summary>
        /// Monthly income.
        /// </summary>
        [Column(TypeName = "decimal(12,2)")]
        public decimal Income { get; set; }

        /// <summary>
        /// Sum on contribution.
        /// </summary>
        [Column(TypeName = "decimal(12,2)")]
        public decimal Sum { get; set; }

        /// <summary>
        /// Contribution identifier.
        /// </summary>
        [ForeignKey("Contribution")]
        public int ContributionId { get; set; }

        /// <summary>
        /// <see cref="Entities.Contribution" /> instance.
        /// </summary>
        public virtual Contribution Contribution { get; set; }
    }
}
