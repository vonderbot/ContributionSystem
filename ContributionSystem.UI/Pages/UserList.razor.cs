using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.User;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    /// <summary>
    /// UserList page code behind.
    /// </summary>
    public partial class UserList : ComponentBase
    {
        [Inject]
        private IUserService UserService { get; set; }

        private IEnumerable<ResponseGetUsersListContributionViewModelItem> _users;
        private string _message;

        /// <inheritdoc /> 
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
                var request = new RequestChangeUserStatusUserViewModel()
                {
                    Id = id,
                    AccountEnabled = newStatus
                };
                await UserService.ChangeUserStatus(request);
                var response = await UserService.GetUsersList();
                _users = response.Items;
            }
            catch (Exception ex)
            {
                _message = ex.Message;
            }
        }
    }
}
