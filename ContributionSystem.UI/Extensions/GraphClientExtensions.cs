﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Authentication.WebAssembly.Msal.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;

/// <summary>
/// Provides methods to add GraphClient.
/// </summary>
internal static class GraphClientExtensions
{
    /// <summary>
    /// Addes GraphClient.
    /// </summary>
    /// <param name="services">IServiceCollection instance.</param>
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
        services.AddScoped(sp =>
        {
            return new GraphServiceClient(
                sp.GetRequiredService<IAuthenticationProvider>(),
                sp.GetRequiredService<IHttpProvider>());
        });

        return services;
    }

    private class NoOpGraphAuthenticationProvider : IAuthenticationProvider
    {
        public NoOpGraphAuthenticationProvider(IAccessTokenProvider tokenProvider)
        {
            TokenProvider = tokenProvider;
        }

        public IAccessTokenProvider TokenProvider { get; }

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var result = await TokenProvider.RequestAccessToken(
                new AccessTokenRequestOptions()
                {
                    Scopes = new[] { "https://graph.microsoft.com/User.Read", "https://graph.microsoft.com/User.Read.All", "https://graph.microsoft.com/profile", "https://graph.microsoft.com/openid"}
                });

            if (result.TryGetToken(out var token))
            {
                request.Headers.Authorization ??= new AuthenticationHeaderValue(
                    "Bearer", token.Value);
            }
        }
    }

    private class HttpClientHttpProvider : IHttpProvider
    {
        private readonly HttpClient http;

        public HttpClientHttpProvider(HttpClient http)
        {
            this.http = http;
        }

        public ISerializer Serializer { get; } = new Serializer();

        public TimeSpan OverallTimeout { get; set; } = TimeSpan.FromSeconds(300);

        public void Dispose()
        {
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return http.SendAsync(request);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            HttpCompletionOption completionOption,
            CancellationToken cancellationToken)
        {
            return http.SendAsync(request, completionOption, cancellationToken);
        }
    }
}