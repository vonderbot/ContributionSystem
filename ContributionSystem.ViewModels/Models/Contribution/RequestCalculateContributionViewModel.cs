using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.ViewModels.Models
{
    public class RequestCalculateContributionViewModel
    {
        public decimal StartValue { get; set; }

        public int Term { get; set; }

        public decimal Percent { get; set; }
    }
}
