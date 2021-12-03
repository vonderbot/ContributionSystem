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
                BaseAddress = new Uri("https://localhost:44303/api/")
            });

            builder.Services.AddScoped<IContributionService, ContributionService>();

            builder.Services.AddMsalAuthentication<RemoteAuthenticationState, 
                CustomUserAccount>(options =>
                {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add("api://3dfbce03-4be6-4a68-9d7f-4c1085202995/API.Access");
                options.ProviderOptions.LoginMode = "redirect";
                })
                .AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomUserAccount, CustomAccountFactory>();

            //builder.Services.AddAuthorizationCore(options =>
            //{
            //    options.AddPolicy("UserAdmin", policy =>
            //        policy.RequireClaim("directoryRole",
            //            "fe930be7-5e62-47db-91af-98c3a49a38b1"));
            //});

            builder.Services.AddGraphClient();

            await builder.Build().RunAsync();
        }
    }
}
