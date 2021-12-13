using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ContributionSystem.UI.Services
{
    public class BaseService
    {
        public readonly HttpClient _http;
        public readonly IAccessTokenProvider _tokenProvider;

        public BaseService(HttpClient httpClient, IAccessTokenProvider tokenProvider)
        {
            _http = httpClient;
            _tokenProvider = tokenProvider;
            AuthorizationHeaderSetup();
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
    }
}
