using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ContributionSystem.UI.Pages
{
    public partial class ContributionCalculator : ComponentBase
    {
        private ResponseCalculateContributionViewModel _responseCalculateContributionViewModel { get; set; }
        private string _errorMessage;
        private string D;
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public async void asd()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var d = user.FindFirst(c => c.Type == "oid")?.Value;
            D = d;
        }
    }
}
