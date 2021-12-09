using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    public partial class UserList : ComponentBase
    {
        [Inject]
        private IContributionService ContributionService { get; set; }

        private IEnumerable<ResponseGetUsersListContributionViewModelItem> _Users;
        private string _message;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await ContributionService.GetUsersList();
                _Users = response.Items;
            }
            catch (Exception ex)
            {
                _message = ex.Message;
            }
        }

        private async Task ChangeUserStatus(string id, bool newStatus)
        {
            try
            {
                var request = new RequestChangeUserStatusContributionViewModel()
                {
                    Id = id,
                    NewStatus = newStatus
                };
                await ContributionService.ChangeUserStatus(request);
                foreach (var user in _Users)
                {
                    if (user.Id == id)
                    {
                        user.Status = newStatus;
                    }
                }
            }
            catch (Exception ex)
            {
                _message = ex.Message;
            }
        }
    }
}
