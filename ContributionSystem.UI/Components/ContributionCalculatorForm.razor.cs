using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Components
{
    public partial class ContributionCalculatorForm : ComponentBase
    {
        [Inject]
        IContributionService ContributionService { get; set; }

        private RequestCalculateContributionViewModel RequestCalculateContributionViewModel { get; set; }

        [Parameter]
        public ResponseCalculateContributionViewModel ResponseCalculateContributionViewModel { get; set; }

        [Parameter]
        public EventCallback<ResponseCalculateContributionViewModel> ResponseCalculateContributionViewModelChanged { get; set; }

        [Parameter]
        public string ErrorMessage { get; set; }

        [Parameter]
        public EventCallback<string> ErrorMessageChanged { get; set; }

        public ContributionCalculatorForm()
        {
            RequestCalculateContributionViewModel = new();
        }

        private async Task Calculate()
        {
            try
            {
                ResponseCalculateContributionViewModel = await ContributionService.Сalculate(RequestCalculateContributionViewModel);
                await ResponseCalculateContributionViewModelChanged.InvokeAsync(ResponseCalculateContributionViewModel);
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                await ErrorMessageChanged.InvokeAsync(ErrorMessage);
            }
        }
    }
}
