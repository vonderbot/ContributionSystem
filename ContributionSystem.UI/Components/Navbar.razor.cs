using Microsoft.AspNetCore.Components;
using ContributionSystem.UI.Constants;

namespace ContributionSystem.UI.Components
{
    /// <summary>
    /// Navbar component code behind.
    /// </summary>
    public partial class Navbar : ComponentBase
    {
        private const string Active = "active";

        private string _calculationButtonIsActive;
        private string _historyButtonIsActive;
        private string _usersButtonIsActive;

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        /// <inheritdoc /> 
        protected override void OnInitialized()
        {
            CheckCurrentUri(out _calculationButtonIsActive, UriConstants.Calculation);
            CheckCurrentUri(out _historyButtonIsActive, UriConstants.History);
            CheckCurrentUri(out _usersButtonIsActive, UriConstants.Users);
        }

        private void CheckCurrentUri(out string buttonIsActive, string uri)
        {
            if (NavigationManager.Uri[(NavigationManager.BaseUri.Length - 1)..].Equals(uri))
            {
                buttonIsActive = Active;
            }
            else
            {
                buttonIsActive = string.Empty;
            }
        }

        private void NavigateToUsersList()
        {
            NavigationManager.NavigateTo(UriConstants.Users);
        }

        private void NavigateToCalculationPage()
        {
            NavigationManager.NavigateTo(UriConstants.Calculation);
        }

        private void NavigateToHistoryPage()
        {
            NavigationManager.NavigateTo(UriConstants.History);
        }
    }
}
