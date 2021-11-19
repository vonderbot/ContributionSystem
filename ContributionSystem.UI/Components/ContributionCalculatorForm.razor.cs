using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Components
{
    public partial class ContributionCalculatorForm : ComponentBase
    {
        [Inject]
        private IContributionService сontributionService { get; set; }

        private RequestCalculateContributionViewModel _requestCalculateContributionViewModel { get; set; }

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
            _requestCalculateContributionViewModel = new RequestCalculateContributionViewModel();
        }

        private async Task Calculate()
        {
            try
            {
                ResponseCalculateContributionViewModel = await сontributionService.Сalculate(_requestCalculateContributionViewModel);
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
