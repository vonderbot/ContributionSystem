using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ContributionSystem.UI.Pages
{
    public partial class ContributionCalculator : ComponentBase
    {
        private ResponseCalculateContributionViewModel _responseCalculateContributionViewModel { get; set; }
        private string _errorMessage;
    }
}
