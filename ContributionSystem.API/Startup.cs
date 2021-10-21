using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ContributionSystem.API.Setup;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;

namespace ContributionSystem.API
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration config)
        {
            configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = true;
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.SetInject();

            services.SetDbContext(configuration);

            services.ConfigureCorsForOrigins(configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(configuration.GetSection("CorsOrigin:AllowOrigins").Value);

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