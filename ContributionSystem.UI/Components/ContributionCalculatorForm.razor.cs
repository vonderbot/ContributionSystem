using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Components
{
    /// <summary>
    /// ContributionCalculatorForm component code behind.
    /// </summary>
    public partial class ContributionCalculatorForm : ComponentBase
    {
        [Inject]
        private IContributionService ContributionService { get; set; }

        [Inject]
        private  AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        /// <summary>
        /// Response model with calculation result.
        /// </summary>
        [Parameter]
        public ResponseCalculateContributionViewModel ResponseCalculateContributionViewModel { get; set; }

        /// <summary>
        /// EventCallback for ResponseCalculateContributionViewModel.
        /// </summary>
        [Parameter]
        public EventCallback<ResponseCalculateContributionViewModel> ResponseCalculateContributionViewModelChanged { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [Parameter]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// EventCallback for ErrorMessage.
        /// </summary>
        [Parameter]
        public EventCallback<string> ErrorMessageChanged { get; set; }

        private RequestCalculateContributionViewModel _requestCalculateContributionViewModel;

        /// <summary>
        /// ContributionCalculatorForm constructor.
        /// </summary>
        public ContributionCalculatorForm()
        {
            _requestCalculateContributionViewModel = new RequestCalculateContributionViewModel();
        }

        private async Task Calculate()
        {
            try
            {
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                if (authState.User.Identity!.IsAuthenticated)
                {
                    ResponseCalculateContributionViewModel = await ContributionService.Сalculate(_requestCalculateContributionViewModel);
                    await ResponseCalculateContributionViewModelChanged.InvokeAsync(ResponseCalculateContributionViewModel);
                }
                else
                {
                    throw new Exception("You must be authorized");
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                await ErrorMessageChanged.InvokeAsync(ErrorMessage);
            }
        }
    }
}
