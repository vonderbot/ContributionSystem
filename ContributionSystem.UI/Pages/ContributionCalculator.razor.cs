using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    public partial class ContributionCalculator : ComponentBase
    {
        private ResponseCalculateContributionViewModel response { get; set; }
        private string errorMessage { get; set; }
    }
}
