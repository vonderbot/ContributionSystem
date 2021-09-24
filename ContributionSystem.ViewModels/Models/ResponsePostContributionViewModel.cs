using ContributionSystem.ViewModels.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.ViewModels.Models
{
    public class ResponsePostContributionViewModel
    {
        [Required]
        public ResponsePostContributionViewModelItem[] Items { get; set; }
    }
}
