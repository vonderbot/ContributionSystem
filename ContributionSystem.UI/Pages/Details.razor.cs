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
        /// Contribution id.
        /// </summary>
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IContributionService ContributionService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private ResponseGetDetailsByIdContributionViewModel _responseGetDetailsByIdContributionViewModel { get; set; }
        private string _message;

        protected void NavigateToHistoryComponent()
        {
            NavigationManager.NavigateTo(URIs.History);
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        protected async Task LoadData()
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
