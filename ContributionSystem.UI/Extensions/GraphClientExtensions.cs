using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Authentication.WebAssembly.Msal.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;

namespace ContributionSystem.UI.Extensions
{
    /// <summary>
    /// Provides methods to add GraphClient.
    /// </summary>
    public static class GraphClientExtensions
    {
        /// <summary>
        /// Addes GraphClient.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection" /> instance.</param>
        /// <param name="scopes">API permissions.</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddGraphClient(
            this IServiceCollection services, params string[] scopes)
        {
            services.Configure<RemoteAuthenticationOptions<MsalProviderOptions>>(
                options =>
                {
                    foreach (var scope in scopes)
                    {
                        options.ProviderOptions.AdditionalScopesToConsent.Add(scope);
                    }
                });

            services.AddScoped<IAuthenticationProvider,
                NoOpGraphAuthenticationProvider>();
            services.AddScoped<IHttpProvider, HttpClientHttpProvider>(sp =>
                new HttpClientHttpProvider(new HttpClient()));
            services.AddScoped(sp => new GraphServiceClient(
                sp.GetRequiredService<IAuthenticationProvider>(),
                sp.GetRequiredService<IHttpProvider>()));

            return services;
        }

        private class NoOpGraphAuthenticationProvider : IAuthenticationProvider
        {
            private const string ScopesSectionName = "Scopes";
            private const string HeaderScheme = "Bearer";

            private readonly IConfiguration _configuration;

            public NoOpGraphAuthenticationProvider(IAccessTokenProvider tokenProvider, IConfiguration configuration)
            {
                TokenProvider = tokenProvider;
                _configuration = configuration;
            }

            public IAccessTokenProvider TokenProvider { get; }

            public async Task AuthenticateRequestAsync(HttpRequestMessage request)
            {
                var result = await TokenProvider.RequestAccessToken(
                    new AccessTokenRequestOptions()
                    {
                        Scopes = _configuration.GetSection(ScopesSectionName).Get<string[]>()
                    });

                if (result.TryGetToken(out var token))
                {
                    request.Headers.Authorization ??= new AuthenticationHeaderValue(
                        HeaderScheme, token.Value);
                }
            }
        }

        private class HttpClientHttpProvider : IHttpProvider
        {
            private readonly HttpClient _http;

            public HttpClientHttpProvider(HttpClient http)
            {
                this._http = http;
            }

            public ISerializer Serializer { get; } = new Serializer();

            public TimeSpan OverallTimeout { get; set; } = TimeSpan.FromSeconds(300);

            public void Dispose()
            {
            }

            public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
            {
                return _http.SendAsync(request);
            }

            public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                HttpCompletionOption completionOption,
                CancellationToken cancellationToken)
            {
                return _http.SendAsync(request, completionOption, cancellationToken);
            }
        }
    }
}