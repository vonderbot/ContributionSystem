using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Components
{
    public partial class ContributionCalculatorTableComponent
    {
        [Parameter]
        public ResponseCalculateContributionViewModel Response { get; set; }

        [Parameter]
        public EventCallback<ResponseCalculateContributionViewModel> ResponseChanged { get; set; }
    }
}
