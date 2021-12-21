using ContributionSystem.UI.Constants;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    /// <summary>
    /// Details page code behind.
    /// </summary>
    public partial class Details : ComponentBase
    {
        /// <summary>
        /// Contribution identifier.
        /// </summary>
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IContributionService ContributionService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private ResponseGetDetailsByIdContributionViewModel _responseGetDetailsByIdContributionViewModel;
        private string _message;

        private void NavigateToHistoryComponent()
        {
            NavigationManager.NavigateTo(UriConstants.History);
        }

        /// <inheritdoc /> 
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                _message = "loading...";
                _responseGetDetailsByIdContributionViewModel = await ContributionService.GetDetailsById(Id);
                _message = null;
            }
            catch (Exception ex)
            {
                _message = ex.Message;
            }
        }
    }
}
