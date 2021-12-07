using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    public partial class UserList : ComponentBase
    {
        [Inject]
        private IContributionService ContributionService { get; set; }

        private IEnumerable<ResponseGetUsersListContributionViewModelItem> _requestsUsers;

        protected override async Task OnInitializedAsync()
        {
            var response = await ContributionService.GetUsersList();
            _requestsUsers = response.Items;
        }
    }
}
