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
        private IUserService UserService { get; set; }

        private IEnumerable<ResponseGetUsersListContributionViewModelItem> _users;
        private string _message;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await UserService.GetUsersList();
                _users = response.Items;

                if (_users == null)
                {
                    _message = "Users data is empty";
                }
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
                    AccountEnabled = newStatus
                };
                await UserService.ChangeUserStatus(request);

                foreach (var user in _users)
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
