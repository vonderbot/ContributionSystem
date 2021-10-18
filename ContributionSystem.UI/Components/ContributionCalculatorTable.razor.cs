using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;

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
