using Microsoft.AspNetCore.Components;
using ContributionSystem.UI.Constants;

namespace ContributionSystem.UI.Components
{
    public partial class Navbar : ComponentBase
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        private void NavigateToCalculationPage()
        {
            navigationManager.NavigateTo(URIs.Calculation);
        }

        private void NavigateToHistoryPage()
        {
            navigationManager.NavigateTo(URIs.History);
        }
    }
}
