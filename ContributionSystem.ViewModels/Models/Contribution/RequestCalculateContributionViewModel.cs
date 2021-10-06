using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContributionSystem.ViewModels.Enums;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class RequestCalculateContributionViewModel
    {
        public CalculationMethodEnumView CalculationMethod { get; set; }

        public decimal StartValue { get; set; }

        public int Term { get; set; }

        public decimal Percent { get; set; }
    }
}
