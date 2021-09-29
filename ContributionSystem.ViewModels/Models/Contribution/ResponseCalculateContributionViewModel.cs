using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class ResponseCalculateContributionViewModel
    {
        public ResponseCalculateContributionViewModelItem[] Items { get; set; }
    }

    public class ResponseCalculateContributionViewModelItem
    {
        public int MonthNumber { get; set; }

        public decimal Income { get; set; }

        public decimal Sum { get; set; }
    }
}
