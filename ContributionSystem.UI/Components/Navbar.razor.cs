using Microsoft.AspNetCore.Components;
using ContributionSystem.UI.Common;

namespace ContributionSystem.UI.Components
{
    public partial class Navbar : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private void NavigateToCalculationPage()
        {
            NavigationManager.NavigateTo(URIs.Calculation);
        }

        private void NavigateToHistoryPage()
        {
            NavigationManager.NavigateTo(URIs.History);
        }
    }
}
