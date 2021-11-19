using ContributionSystem.UI.Common;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    public partial class Details : ComponentBase
    {
        [Inject]
        private IContributionService сontributionService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int Id { get; set; }

        private ResponseGetDetailsByIdContributionViewModel _responseGetDetailsByIdContributionViewModel { get; set; }
        private string _message;

        public void NavigateToHistoryComponent()
        {
            NavigationManager.NavigateTo(URIs.History);
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        public async Task LoadData()
        {
            try
            {
                _message = "loading...";
                _responseGetDetailsByIdContributionViewModel = await сontributionService.GetDetailsById(Id);
                _message = null;
            }
            catch (Exception ex)
            {
                _message = ex.Message;
            }
        }
    }
}
