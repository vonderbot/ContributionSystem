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

        private IEnumerable<RequestCalculateContributionViewModel> _requestsHistory;
        private IEnumerable<MemberInfo> _fieldlist;
        private string _message;
        private bool _outOfData;
        private int _numberOfLoads;

        private async Task LoadData() 
        {
            try
            {
                _message = "loading...";
                var response = await ContributionService.GetRequestsHistory(NumberOfContrbutionForOneLoad, _numberOfLoads * NumberOfContrbutionForOneLoad);

                if (response == null || response.Count < NumberOfContrbutionForOneLoad)
                {
                    _outOfData = true;
                    if (_numberOfLoads == 0 && response == null)
                    {
                        _message = "History is empty";
                    }
                    else
                    {
                        _message = "End of history";
                    }
                }
                else
                {
                    _message = null;
                }
                _requestsHistory = _requestsHistory.Concat(response);
                _fieldlist = typeof(RequestCalculateContributionViewModel).GetMembers()
                    .Where(mi => mi.MemberType == MemberTypes.Field ||
                    mi.MemberType == MemberTypes.Property);
                _numberOfLoads++;
            }
            catch (Exception ex)
            {
                _outOfData = true;
                _message = ex.Message;
            }
        }

        public async Task LoadMore()
        {
            await LoadData();
        }

        protected override async Task OnInitializedAsync()
        {
            _requestsHistory = new List<RequestCalculateContributionViewModel>();
            _numberOfLoads = 0;
            _outOfData = false;
            await LoadData();
        }
    }
}