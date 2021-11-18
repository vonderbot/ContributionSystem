using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    public partial class History : ComponentBase
    {
        [Inject]
        IContributionService ContributionService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private int _take;
        private int _skip;
        private IEnumerable<ResponseGetHistoryContributionViewModelItem> _requestsHistory;
        private string _message;

        public void NavigateToDetailsComponent(int id)
        {
            NavigationManager.NavigateTo($"Details/{id}");
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
                var response = await ContributionService.GetHistory(_take, _skip);
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