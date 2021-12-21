using System.Net.Http;
using ContributionSystem.UI.Providers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Authentication.WebAssembly.Msal.Models;
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
        public static void AddGraphClient(
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
                CustomAuthenticationProvider>();
            services.AddScoped<IHttpProvider, CustomHttpProvider>(sp =>
                new CustomHttpProvider(new HttpClient()));
            services.AddScoped(sp => new GraphServiceClient(
                sp.GetRequiredService<IAuthenticationProvider>(),
                sp.GetRequiredService<IHttpProvider>()));
        }
    }
}