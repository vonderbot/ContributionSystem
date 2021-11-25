using ContributionSystem.UI.Interfaces;
using ContributionSystem.UI.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ContributionSystem.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp =>
            new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44303/api/")
            });

            builder.Services.AddScoped<IContributionService, ContributionService>();

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add("api://ac090f55-725f-4ce0-be8b-d2e5d6b300bf/TestScope");
                builder.Services.Configure<IdentityOptions>(options =>
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);
            });

            await builder.Build().RunAsync();
        }
    }
}
