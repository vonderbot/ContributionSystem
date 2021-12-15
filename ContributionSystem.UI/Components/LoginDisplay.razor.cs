using ContributionSystem.UI.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Graph;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace ContributionSystem.UI.Components
{
    /// <summary>
    /// LoginDisplay component code behind.
    /// </summary>
    public partial class LoginDisplay : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private SignOutSessionStateManager SignOutManager { get; set; }

        private async Task BeginLogout()
        {
            await SignOutManager.SetSignOutState();
            NavigationManager.NavigateTo("authentication/logout");
        }

        private void RedirectToLogin()
        {
            NavigationManager.NavigateTo($"{UriConstants.Login}?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}");
        }
    }
}
