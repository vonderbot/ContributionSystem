using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.BusinessLogic.Services;
using ContributionSystem.UI.Validators;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContributionSystem.API.Setup
{
    public static class Services
    {
        public static void SetInject(this IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = true;
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
            services.AddTransient<IValidator<RequestCalculateContributionViewModel>, RequestCalculateContributionViewModelValidator>();
            services.AddScoped<IContributionService, ContributionService>();
        }
    }
}
