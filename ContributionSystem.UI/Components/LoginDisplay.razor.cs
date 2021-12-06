﻿using ContributionSystem.UI.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Components
{
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
            NavigationManager.NavigateTo($"{URIs.Login}?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}");
        }
    }
}
