using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using FluentValidation;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.ViewModels.Validators;
using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.BusinessLogic.Services;

namespace ContributionSystem.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = true;
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.AddTransient<IValidator<RequestCalculateContributionViewModel>, RequestCalculateContributionViewModelValidator>();
            services.AddScoped<IContributionService, ContributionService>();
            //services.AddScoped<ICon, ContributionService>();
        }

        // This method gets called by the runtime. Use this me
        // thod to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(cors => cors
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}