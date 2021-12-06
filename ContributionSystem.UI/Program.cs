using ContributionSystem.UI.Interfaces;
using ContributionSystem.UI.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

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
                BaseAddress = new Uri(builder.Configuration.GetSection("BaseAddress").Value)
            });

            builder.Services.AddScoped<IContributionService, ContributionService>();

            builder.Services.AddMsalAuthentication<RemoteAuthenticationState, 
                CustomUserAccount>(options =>
                {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add(builder.Configuration.GetSection("DefaultAccessTokenScopes").Value);
                options.ProviderOptions.LoginMode = "redirect";
                options.UserOptions.RoleClaim = "appRole";
                })
                .AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomUserAccount, CustomAccountFactory>();

            builder.Services.AddGraphClient();

            await builder.Build().RunAsync();
        }
    }
}
