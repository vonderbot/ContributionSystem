using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    public partial class History : ComponentBase
    {
        [Inject]
        IContributionService ContributionService { get; set; }

        private const int NumberOfContrbutionForOneLoad = 8;

        private IEnumerable<ResponseGetRequestsHistoryContributionViewModel> _requestsHistory;
        private IEnumerable<MemberInfo> _fieldlist;
        private string _message;
        private int _numberOfLoads;

        public async Task LoadMore()
        {
            await LoadData();
        }

        protected override async Task OnInitializedAsync()
        {
            _fieldlist = typeof(ResponseGetRequestsHistoryContributionViewModel).GetMembers()
                    .Where(mi => mi.MemberType == MemberTypes.Field ||
                    mi.MemberType == MemberTypes.Property);
            _requestsHistory = new List<ResponseGetRequestsHistoryContributionViewModel>();
            _numberOfLoads = 0;
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                _message = "loading...";
                var response = await ContributionService.GetRequestsHistory(NumberOfContrbutionForOneLoad, _numberOfLoads * NumberOfContrbutionForOneLoad);

                if (_numberOfLoads == 0 && response == null)
                {
                    _message = "History is empty";
                }
                else if (response == null || response.Count < NumberOfContrbutionForOneLoad)
                {
                    _message = "End of history";
                }
                else
                {
                    _message = null;
                }
                _requestsHistory = _requestsHistory.Concat(response);
                _numberOfLoads++;
            }
            catch (Exception ex)
            {
                _message = ex.Message;
            }
        }
    }
}