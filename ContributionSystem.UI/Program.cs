using ContributionSystem.UI.CustomAccounts;
using ContributionSystem.UI.Extensions;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.UI.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContributionSystem.UI
{
    public class Program
    {
        private const string AuthenticationOptionSectionName = "AzureAd";
        private const string AccessTokenScopeSectionName = "DefaultAccessTokenScope";
        private const string LoginMode = "redirect";
        private const string RoleClaim = "roles";

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped( _ =>
            new HttpClient
            {
                BaseAddress = new Uri(builder.Configuration.GetSection("BaseAddress").Value)
            });

            builder.Services.AddScoped<IContributionService, ContributionService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddMsalAuthentication<RemoteAuthenticationState, 
                CustomUserAccount>(options =>
                {
                builder.Configuration.Bind(AuthenticationOptionSectionName, options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add(builder.Configuration.GetSection(AccessTokenScopeSectionName).Value);
                options.ProviderOptions.LoginMode = LoginMode;
                options.UserOptions.RoleClaim = RoleClaim;
                })
                .AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomUserAccount, CustomAccountFactory>();

            builder.Services.AddGraphClient();

            await builder.Build().RunAsync();
        }
    }
}
