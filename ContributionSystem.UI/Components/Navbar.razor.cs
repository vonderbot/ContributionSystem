using Microsoft.AspNetCore.Components;
using ContributionSystem.UI.Constants;

namespace ContributionSystem.UI.Components
{
    public partial class Navbar : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private string _calculationButtonExtraClasses;
        private string _historyButtonExtraClasses;
        private string _usersButtonExtraClasses;

        protected override void OnInitialized()
        {
            if (NavigationManager.Uri[(NavigationManager.BaseUri.Length - 1)..] == URIs.Calculation)
            {
                _calculationButtonExtraClasses += "active ";
            }
            else if (NavigationManager.Uri[(NavigationManager.BaseUri.Length - 1)..] == URIs.History)
            {
                _historyButtonExtraClasses += "active ";
            }
            else if (NavigationManager.Uri[(NavigationManager.BaseUri.Length - 1)..] == URIs.Users)
            {
                _usersButtonExtraClasses += "active ";
            }
        }

        private void NavigateToUsersList()
        {
            NavigationManager.NavigateTo(URIs.Users);
        }

        private void NavigateToCalculationPage()
        {
            NavigationManager.NavigateTo(URIs.Calculation);
        }

        private void NavigateToHistoryPage()
        {
            NavigationManager.NavigateTo(URIs.History);
        }
    }
}
