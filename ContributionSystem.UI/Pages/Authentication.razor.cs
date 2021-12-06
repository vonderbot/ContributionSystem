using ContributionSystem.UI.Constants;
using Microsoft.AspNetCore.Components;

namespace ContributionSystem.UI.Pages
{
    public partial class Authentication : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Action { get; set; }

        private void RedirectToMain()
        {
            NavigationManager.NavigateTo(URIs.Calculation);
        }
    }
}
