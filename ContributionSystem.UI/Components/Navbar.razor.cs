using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ContributionSystem.UI.Components
{
    public partial class Navbar : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public void NavigateToCalculationPage()
        {
            NavigationManager.NavigateTo("/Main");
        }

        public void NavigateToHistoryPage()
        {
            NavigationManager.NavigateTo("/History");
        }
    }
}
