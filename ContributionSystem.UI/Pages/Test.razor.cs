using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    public class RequestCalculateContributionViewModel : ComponentBase
    {
        public decimal StartValue { get; set; }

        public int Term { get; set; }

        public decimal Percent { get; set; }
    }

}
