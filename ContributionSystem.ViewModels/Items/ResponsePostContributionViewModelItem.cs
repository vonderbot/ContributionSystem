using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.ViewModels.Items
{
    public class ResponsePostContributionViewModelItem
    {
        [Required]
        public int MonthNumber { get; set; }
        [Required]
        [RegularExpression(@"^\-?[0-9]+(?:\.[0-9]{1,2})?$")]
        public decimal Income { get; set; }
        [Required]
        [RegularExpression(@"^\-?[0-9]+(?:\.[0-9]{1,2})?$")]
        public decimal Sum { get; set; }
    }
}
