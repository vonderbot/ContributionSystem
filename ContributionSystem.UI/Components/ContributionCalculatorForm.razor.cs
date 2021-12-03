﻿using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Components
{
    public partial class ContributionCalculatorForm : ComponentBase
    {
        [Inject]
        private IContributionService ContributionService { get; set; }

        [Inject]
        private  AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        IAccessTokenProvider TokenProvider { get; set; }

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
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                if (authState.User.Identity.IsAuthenticated)
                {
                    var tokenResult = await TokenProvider.RequestAccessToken();
                    tokenResult.TryGetToken(out var token);
                    var g = token.Value;
                    _requestCalculateContributionViewModel.UserId = authState.User.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
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
