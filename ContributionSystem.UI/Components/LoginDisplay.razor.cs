using ContributionSystem.UI.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Graph;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace ContributionSystem.UI.Components
{
    public partial class LoginDisplay : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private SignOutSessionStateManager SignOutManager { get; set; }

        [Inject]
        private GraphServiceClient GraphClient { get; set; }

        //private string role; 

        //protected async Task GetRole()
        //{
        //    var request = GraphClient.Me.Request();
        //    User user = await request.GetAsync();
        //    var roles = user.AppRoleAssignments;
        //    foreach (var item in roles)
        //    {
        //        role += $"{item.Id} ";
        //    }
        //}

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
