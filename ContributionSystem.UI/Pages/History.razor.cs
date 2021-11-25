using ContributionSystem.UI.Common;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    public partial class History : ComponentBase
    {
        [Inject]
        private IContributionService сontributionService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private int _take;
        private int _skip;
        private IEnumerable<ResponseGetHistoryContributionViewModelItem> _requestsHistory;
        private string _message;

        public async void asd()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var d = authState.User.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
        }

        public void NavigateToDetailsComponent(int id)
        {
            NavigationManager.NavigateTo($"{URIs.Details}/{id}");
        }

        public async Task LoadMore()
        {
            await LoadData();
        }

        protected override async Task OnInitializedAsync()
        {
            _take = 8;
            _skip = 0;
            _requestsHistory = new List<ResponseGetHistoryContributionViewModelItem>();
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                _message = "loading...";
                var response = await сontributionService.GetHistory(_take, _skip);
                _skip = response.Take + response.Skip;

                if (_skip >= response.TotalNumberOfRecords && response.TotalNumberOfRecords != 0)
                {
                    _message = "End of history";
                }
                else if(response.TotalNumberOfRecords == 0)
                {
                    _message = "History is empty";
                }
                else
                {
                    _message = null;
                }

                _requestsHistory = _requestsHistory.Concat(response.Items.ToList());
            }
            catch (Exception ex)
            {
                _message = ex.Message;
            }
        }
    }
}