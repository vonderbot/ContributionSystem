using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    public partial class ContributionCalculator : ComponentBase
    {
        [Inject]
        IContributionService ContributionCalculatorService { get; set; }
        private RequestCalculateContributionViewModel Request;
        private ResponseCalculateContributionViewModel Content;
        private string ErrorMessage;

        public ContributionCalculator()
        {
            Request = new();
        }

        public async Task Calculate()
        {
            try
            {
                Content = await ContributionCalculatorService.Сalculate(Request);
            }
            catch
            {
                ErrorMessage = "Error!";
            }
        }
    }
}
