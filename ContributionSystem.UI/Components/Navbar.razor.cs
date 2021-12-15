using Microsoft.AspNetCore.Components;
using ContributionSystem.UI.Constants;

namespace ContributionSystem.UI.Components
{
    /// <summary>
    /// Navbar component code behind.
    /// </summary>
    public partial class Navbar : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private string _calculationButtonExtraClasses;
        private string _historyButtonExtraClasses;
        private string _usersButtonExtraClasses;

        protected override void OnInitialized()
        {
            if (NavigationManager.Uri[(NavigationManager.BaseUri.Length - 1)..] == UriConstants.Calculation)
            {
                _calculationButtonExtraClasses += "active ";
            }
            else if (NavigationManager.Uri[(NavigationManager.BaseUri.Length - 1)..] == UriConstants.History)
            {
                _historyButtonExtraClasses += "active ";
            }
            else if (NavigationManager.Uri[(NavigationManager.BaseUri.Length - 1)..] == UriConstants.Users)
            {
                _usersButtonExtraClasses += "active ";
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
