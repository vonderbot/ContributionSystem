using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;

namespace ContributionSystem.API.Extensions
{
    /// <summary>
    /// Provides methods for setting authorization.
    /// </summary>
    public static class ServiceCollectionAuthorizationExtension
    {
        /// <summary>
        /// Provides azure active directory authentification.
        /// </summary>
        /// <param name="services">IServiceCollection instance.</param>
        /// <param name="configuration">IConfiguration instance.</param>
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
                        ValidateIssuer = true,
                    };
                });
        }
    }
}
