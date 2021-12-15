using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ContributionSystem.UI.Services
{
    /// <summary>
    /// Provides base instances for services.
    /// </summary>
    public class BaseService
    {
        private const string HeaderScheme = "Bearer";

        protected readonly HttpClient Http;
        protected readonly IAccessTokenProvider TokenProvider;

        /// <summary>
        /// Creates a new instance of <see cref="BaseService" />.
        /// </summary>
        /// <param name="httpClient"><see cref="HttpClient" /> instance.</param>
        /// <param name="tokenProvider"><see cref="IAccessTokenProvider" /> instance.</param>
        protected BaseService(HttpClient httpClient, IAccessTokenProvider tokenProvider)
        {
            Http = httpClient;
            TokenProvider = tokenProvider;
            AuthorizationHeaderSetup();
        }

        private async void AuthorizationHeaderSetup()
        {
            var tokenResult = await TokenProvider.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(HeaderScheme, token.Value);
            }
            else
            {
                throw new Exception("Cant get access token");
            }
        }
    }
}
