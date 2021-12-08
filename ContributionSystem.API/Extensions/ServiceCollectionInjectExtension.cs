using Azure.Identity;
using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.BusinessLogic.Services;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.DataAccess.Repositories;
using ContributionSystem.UI.Validators;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;

namespace ContributionSystem.API.Extensions
{
    public static class ServiceCollectionInjectExtension
    {
        public static void SetInject(this IServiceCollection services)
        {
            services.AddTransient<IValidator<RequestCalculateContributionViewModel>, RequestCalculateContributionViewModelValidator>();
            services.AddScoped<IContributionService, ContributionService>();
            services.AddScoped<IContributionRepository, ContributionRepository>();
            services.AddScoped<IUserService, UserService>();
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var tenantId = "06f7343a-04fe-482f-8992-ecc288985e9c";
            var clientId = "3dfbce03-4be6-4a68-9d7f-4c1085202995";
            var clientSecret = "OmB7Q~67dMTTUQGZ4-jZxAjcl64ZnBPt-TeVK";
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
