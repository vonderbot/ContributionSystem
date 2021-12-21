using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;

namespace ContributionSystem.UI.Pages
{
    /// <summary>
    /// ContributionCalculator page code behind.
    /// </summary>
    public partial class ContributionCalculator : ComponentBase
    {
        private ResponseCalculateContributionViewModel _responseCalculateContributionViewModel;
        private string _errorMessage;
    }
}
