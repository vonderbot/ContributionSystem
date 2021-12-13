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
    /// <inheritdoc/>
    public class UserService : BaseService, IUserService
    {
        private const string СontrollerName = "user";

        /// <summary>
        /// UserService constructor.
        /// </summary>
        /// <param name="httpClient">HttpClient instance.</param>
        /// <param name="tokenProvider">IAccessTokenProvider instance.</param>
        public UserService(HttpClient httpClient, IAccessTokenProvider tokenProvider)
            :base(httpClient, tokenProvider)
        {
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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