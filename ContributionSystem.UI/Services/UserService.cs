using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace ContributionSystem.UI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;
        private readonly IAccessTokenProvider _tokenProvider;

        private const string СontrollerName = "user";

        public UserService(HttpClient httpClient, IAccessTokenProvider tokenProvider)
        {
            _http = httpClient;
            _tokenProvider = tokenProvider;
            AuthorizationHeaderSetup();
        }

        public async Task ChangeUserStatus(RequestChangeUserStatusContributionViewModel request)
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

        public async Task<ResponseGetUsersListContributionViewModel> GetUsersList()
        {
            try
            {
                var response = await _http.GetAsync($"{СontrollerName}/GetUsersList");
                await CheckResponseStatusCode(response);
                var details = await response.Content.ReadFromJsonAsync<ResponseGetUsersListContributionViewModel>();

                return details;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception in service: {ex.Message}");
            }
        }

        private async void AuthorizationHeaderSetup()
        {
            var tokenResult = await _tokenProvider.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
            }
            else
            {
                throw new Exception("Cant get access token");
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