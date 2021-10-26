using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class ResponseGetRequestsHistoryContributionViewModel
    {
        public decimal Percent { get; set; }

        public int Term { get; set; }

        public decimal Sum { get; set; }

        public string Date { get; set; }
    }
}
