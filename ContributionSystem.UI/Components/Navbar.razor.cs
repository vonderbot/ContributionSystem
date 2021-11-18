using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace ContributionSystem.UI.Components
{
    public partial class Navbar : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public void NavigateToCalculationPage()
        {
            NavigationManager.NavigateTo("/Main");
        }

        public void NavigateToHistoryPage()
        {
            var b = NavigationManager.Uri;
            NavigationManager.NavigateTo("/History");
        }
    }
}
