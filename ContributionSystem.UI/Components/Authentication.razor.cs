using ContributionSystem.UI.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ContributionSystem.UI.Components
{
    public partial class Authentication : ComponentBase
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Parameter]
        public string Action { get; set; }

        public void RedirectToMain()
        {
            navigationManager.NavigateTo(URIs.Calculation);
        }
    }
}
