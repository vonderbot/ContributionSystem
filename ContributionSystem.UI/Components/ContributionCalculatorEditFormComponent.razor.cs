using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Components
{
    public partial class ContributionCalculatorEditFormComponent : ComponentBase
    {
        [Inject]
        IContributionService ContributionCalculatorService { get; set; }

        [Parameter]
        public RequestCalculateContributionViewModel Request { get; set; }

        [Parameter]
        public ResponseCalculateContributionViewModel Response { get; set; }

        [Parameter]
        public EventCallback<ResponseCalculateContributionViewModel> ResponseChanged { get; set; }

        [Parameter]
        public string ErrorMessage { get; set; }

        [Parameter]
        public EventCallback<string> ErrorMessageChanged { get; set; }

        public ContributionCalculatorEditFormComponent()
        {
            Request = new();
        }

        public async Task Calculate()
        {
            try
            {
                Response = await ContributionCalculatorService.Сalculate(Request);
                await ResponseChanged.InvokeAsync(Response);
            }
            catch
            {
                ErrorMessage = "Error!";
                await ErrorMessageChanged.InvokeAsync(ErrorMessage);
            }
        }
    }
}
