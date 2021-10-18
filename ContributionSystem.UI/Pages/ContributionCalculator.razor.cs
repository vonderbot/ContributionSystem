using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using ContributionSystem.ViewModels.Enums;
using System;

namespace ContributionSystem.UI.Pages
{
    public partial class ContributionCalculator : ComponentBase
    {
        private ResponseCalculateContributionViewModel _responseCalculateContributionViewModel { get; set; }
        private string _errorMessage;
    }
}
