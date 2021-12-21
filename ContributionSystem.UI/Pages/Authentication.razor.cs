using ContributionSystem.UI.Constants;
using Microsoft.AspNetCore.Components;

namespace ContributionSystem.UI.Pages
{
    /// <summary>
    /// Authentication page code behind.
    /// </summary>
    public partial class Authentication : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// Action for RemoteAuthenticatorView.
        /// </summary>
        [Parameter]
        public string Action { get; set; }

        private void RedirectToMain()
        {
            NavigationManager.NavigateTo(UriConstants.Calculation);
        }
    }
}
