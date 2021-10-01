using ContributionSystem.UI.Services;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    public class ContributionCalculatorComponent : ComponentBase
    {
        private static ContributionCalculatorService contributionCalculatorService = new();

        public RequestCalculateContributionViewModel request = new();

        public ResponseCalculateContributionViewModel content;

        public string errorMessage;

        public async Task Calculate()
        {
            var response = await contributionCalculatorService.СontributionСalculate(request);
            if (((int)response.StatusCode) == 200)
            {
                content = await response.Content.ReadFromJsonAsync<ResponseCalculateContributionViewModel>();
            }
            else
            {
                errorMessage = "Error!";
            }
        }
    }
}
