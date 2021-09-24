using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.ViewModels.Models
{
    public class RequestPostContributionViewModel
    {
        [Required]
        [RegularExpression(@"^\-?[0-9]+(?:\.[0-9]{1,2})?$")]
        public decimal StartValue { get; set; }
        [Required]
        public int Term { get; set; }
        [Required]
        [RegularExpression(@"^\-?[0-9]+(?:\.[0-9]{1,2})?$")]
        public decimal Percent { get; set; }
    }
}
