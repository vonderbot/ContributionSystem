using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Components
{
    public partial class ContributionCalculatorTable
    {
        [Parameter]
        public ResponseCalculateContributionViewModel ResponseCalculateContributionViewModel { get; set; }

        [Parameter]
        public EventCallback<ResponseCalculateContributionViewModel> ResponseCalculateContributionViewModelChanged { get; set; }
    }
}
