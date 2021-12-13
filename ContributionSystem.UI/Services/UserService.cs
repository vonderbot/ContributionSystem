using ContributionSystem.UI.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using ContributionSystem.ViewModels.Models.User;

namespace ContributionSystem.UI.Services
{
    public class UserService : BaseService, IUserService
    {
        private const string СontrollerName = "user";

        public UserService(HttpClient httpClient, IAccessTokenProvider tokenProvider)
            :base(httpClient, tokenProvider)
        {
        }

        public async Task ChangeUserStatus(RequestChangeUserStatusUserViewModel request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync($"{СontrollerName}/changeuserstatus", request);
                await CheckResponseStatusCode(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception in service: {ex.Message}");
            }
        }

        public async Task<ResponseGetUsersListUserViewModel> GetUsersList()
        {
            try
            {
                var response = await _http.GetAsync($"{СontrollerName}/GetUsersList");
                await CheckResponseStatusCode(response);
                var details = await response.Content.ReadFromJsonAsync<ResponseGetUsersListUserViewModel>();

                return details;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception in service: {ex.Message}");
            }
        }

        private async Task CheckResponseStatusCode(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var exception = await response.Content.ReadAsStringAsync();

                throw new Exception(exception);
            }
        }
    }
}