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

        private RequestCalculateContributionViewModel request;
        private ResponseCalculateContributionViewModel response;
        private string errorMessage;

        public ContributionCalculator()
        {
            request = new();
        }

        public async Task Calculate()
        {
            try
            {
                response = await ContributionCalculatorService.Сalculate(request);
            }
            catch
            {
                errorMessage = "Error!";
            }
        }
    }
}
