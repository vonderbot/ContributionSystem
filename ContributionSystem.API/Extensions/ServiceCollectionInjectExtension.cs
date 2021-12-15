using Azure.Identity;
using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.BusinessLogic.Services;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.DataAccess.Repositories;
using ContributionSystem.UI.Validators;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;

namespace ContributionSystem.API.Extensions
{
    /// <summary>
    /// Provides methods for setting injects.
    /// </summary>
    public static class ServiceCollectionInjectExtension
    {
        /// <summary>
        /// Sets dependency injection.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection" /> instance.</param>
        /// <param name="configuration"><see cref="IConfiguration" /> instance.</param>
        public static void SetInject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IValidator<RequestCalculateContributionViewModel>, RequestCalculateContributionViewModelValidator>();
            services.AddScoped<IContributionService, ContributionService>();
            services.AddScoped<IContributionRepository, ContributionRepository>();
            services.AddScoped<IUserService, UserService>();
            SetGraphServiceClientInject(services, configuration);
        }

        private static void SetGraphServiceClientInject(IServiceCollection services, IConfiguration configuration)
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var tenantId = configuration["AzureAd:TenantId"];
            var clientId = configuration["AzureAd:ClientId"]; ;
            var clientSecret = configuration["AzureAd:ClientSecret"];
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);
            services.AddScoped<GraphServiceClient>(sp => {
                return new GraphServiceClient(clientSecretCredential, scopes);
            });
        }
    }
}
