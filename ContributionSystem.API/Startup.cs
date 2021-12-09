using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using ContributionSystem.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using ContributionSystem.API.Extensions;
using Microsoft.Graph.ExternalConnectors;

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

            services.SetInject(configuration);

            var connection = configuration.GetConnectionString("DefaultConnection");


            services.AddDbContext<ContributionDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddAzureAdAuthentication(configuration);

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}