using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.BusinessLogic.Services;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.DataAccess.Repositories;
using ContributionSystem.UI.Validators;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ContributionSystem.API.Extensions
{
    public static class ServiceCollectionInjectExtension
    {
        public static void SetInject(this IServiceCollection services)
        {
            services.AddTransient<IValidator<RequestCalculateContributionViewModel>, RequestCalculateContributionViewModelValidator>();
            services.AddScoped<IContributionService, ContributionService>();
            services.AddScoped<IContributionRepository, ContributionRepository>();
        }
    }
}
