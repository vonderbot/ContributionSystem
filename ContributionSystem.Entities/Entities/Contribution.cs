using ContributionSystem.Entities.Enums;
using System;
using System.Collections.Generic;

namespace ContributionSystem.Entities.Entities
{
    public class Contribution
    {
        public int Id { get; set; }

        public decimal StartValue { get; set; }

        public int Term { get; set; }

        public decimal Percent { get; set; }

        public DateTime Date { get; set; }

        public CalculationMethodEnum CalculationMethod { get; set; }

        public ICollection<MonthInfo> Details { get; set; }

        public Contribution()
        {
            Details = new List<MonthInfo>();
        }
    }
}
