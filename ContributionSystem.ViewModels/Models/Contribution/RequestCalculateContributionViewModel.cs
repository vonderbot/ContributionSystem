using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class RequestCalculateContributionViewModel
    {

        public int MethodNumber { get; set; }

        public decimal StartValue { get; set; }

        public int Term { get; set; }

        public decimal Percent { get; set; }
    }
}
