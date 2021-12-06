using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        }
    }
}
