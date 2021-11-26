using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Components
{
    public partial class LoginDisplay : ComponentBase
    {
        [Inject]
        private NavigationManager navigation { get; set; }

        [Inject]
        private SignOutSessionStateManager signOutManager { get; set; }

        private async Task BeginLogout(MouseEventArgs args)
        {
            await signOutManager.SetSignOutState();
            navigation.NavigateTo("authentication/logout");
        }
    }
}
