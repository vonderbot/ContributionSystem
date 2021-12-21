using ContributionSystem.UI.Constants;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    /// <summary>
    /// History page code behind.
    /// </summary>
    public partial class History : ComponentBase
    {
        private int _take;
        private int _skip;
        private IEnumerable<ResponseGetUsersListContributionViewModelItems> _requestsHistory;
        private string _message;

        [Inject]
        private IContributionService ContributionService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IConfiguration Configuration { get; set; }

        private void NavigateToDetailsComponent(int id)
        {
            NavigationManager.NavigateTo($"{UriConstants.Details}/{id}");
        }

        /// <inheritdoc /> 
        protected override async Task OnInitializedAsync()
        {
            _take = Configuration.GetValue<int>("Take");
            _requestsHistory = new List<ResponseGetUsersListContributionViewModelItems>();
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                _message = "loading...";
                var response = await ContributionService.GetHistoryByUserId(_take, _skip);
                _skip = response.Take + response.Skip;

                if (_skip >= response.TotalNumberOfUserRecords && response.TotalNumberOfUserRecords != 0)
                {
                    _message = "End of history";
                }
                else if(response.TotalNumberOfUserRecords == 0)
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