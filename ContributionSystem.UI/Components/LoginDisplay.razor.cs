using ContributionSystem.UI.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Components
{
    public partial class LoginDisplay : ComponentBase
    {
        [Inject]
        private NavigationManager navigation { get; set; }

        [Inject]
        private SignOutSessionStateManager signOutManager { get; set; }

        private async Task BeginLogout()
        {
            await signOutManager.SetSignOutState();
            navigation.NavigateTo("authentication/logout");
        }

        private void RedirectToLogin()
        {
            navigation.NavigateTo($"{URIs.Login}?returnUrl={Uri.EscapeDataString(navigation.Uri)}");
        }
    }
}
