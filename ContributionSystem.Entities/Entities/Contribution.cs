using ContributionSystem.Entities.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContributionSystem.Entities.Entities
{
    /// <summary>
    /// General information about contrbution.
    /// </summary>
    public class Contribution : BaseEntity
    {
        /// <summary>
        /// Contribution constructor.
        /// </summary>
        public Contribution()
        {
            Details = new List<MonthInfo>();
        }

        /// <summary>
        /// Start value.
        /// </summary>
        [Column(TypeName = "decimal(12,2)")]
        public decimal StartValue { get; set; }

        /// <summary>
        /// Term in months.
        /// </summary>
        public int Term { get; set; }

        /// <summary>
        /// Percent per year.
        /// </summary>
        [Column(TypeName = "decimal(6,2)")]
        public decimal Percent { get; set; }

        /// <summary>
        /// Date of creating calculation.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Id of user, who made calculation.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Calculation method.
        /// </summary>
        public CalculationMethodEnum CalculationMethod { get; set; }

        /// <summary>
        /// Months info for Calculation.
        /// </summary>
        public virtual IEnumerable<MonthInfo> Details { get; set; }
    }
}
