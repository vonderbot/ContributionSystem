using ContributionSystem.Entities.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContributionSystem.Entities.Entities
{
    public class Contribution : BaseEntity
    {
        [Column(TypeName = "decimal(12,2)")]
        public decimal StartValue { get; set; }

        public int Term { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal Percent { get; set; }

        public string Date { get; set; }

        public string UserId { get; set; }

        public CalculationMethodEnum CalculationMethod { get; set; }

        public IEnumerable<MonthInfo> Details { get; set; }

        public Contribution()
        {
            Details = new List<MonthInfo>();
        }
    }
}
