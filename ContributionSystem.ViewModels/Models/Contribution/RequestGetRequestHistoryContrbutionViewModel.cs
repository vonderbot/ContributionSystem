using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class RequestGetRequestHistoryContrbutionViewModel
    {
        public int NumberOfContrbutionForLoad { get; set; }

        public int NumberOfContrbutionForSkip { get; set; }
    }
}
