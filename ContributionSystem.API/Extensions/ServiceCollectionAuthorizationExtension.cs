using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;

namespace ContributionSystem.API.Extensions
{
    public static class ServiceCollectionAuthorizationExtension
    {
        public static void AddAzureAdAuthentication(this IServiceCollection services,
                                                         IConfiguration configuration)
        {
            var instance = configuration["AzureAd:Instance"];
            var tenantId = configuration["AzureAd:TenantId"];
            var audience = configuration["AzureAd:Audience"];

            //services
            //    .AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddMicrosoftIdentityWebApi(options =>
            //    {
            //        options.Authority = string.Format(instance, tenantId);
            //        options.Audience = audience;
            //        options.TokenValidationParameters.RoleClaimType = "wids";
            //    }, 
            //    options => 
            //    {
            //        configuration.Bind("AzureAd", options); 
            //    });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(options =>
            //    {
            //        configuration.Bind("AzureAd", options);
            //        options.TokenValidationParameters.RoleClaimType =
            //        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            //    },
            //    options => { configuration.Bind("AzureAd", options); });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = string.Format(instance, tenantId);
                    options.Audience = audience;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        SaveSigninToken = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidateIssuer = true
                    };
                });
            services.AddAuthorization(options => {
                options.AddPolicy("UserAdmin", policy => 
                policy.RequireClaim("wids", configuration["Wids:UserAdmin"]));
            });
        }
    }
}
