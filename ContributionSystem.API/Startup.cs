using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using ContributionSystem.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using ContributionSystem.API.Extensions;

namespace ContributionSystem.API
{
    /// <summary>
    /// Provides the entry point for the application.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Creates a new instance of <see cref="Startup" />.
        /// </summary>
        /// <param name="config"><see cref="IConfiguration" /> instance.</param>
        public Startup(IConfiguration config)
        {
            _configuration = config;
        }

        /// <summary>
        /// Configures services that are used by application.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection" /> instance.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = true;
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.SetInject(_configuration);

            var connection = _configuration.GetConnectionString("DefaultConnection");


            services.AddDbContext<ContributionDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddAzureAdAuthentication(_configuration);

            services.ConfigureCorsForOrigins(_configuration);
        }

        /// <summary>
        /// Specifies how the application will respond to individual HTTP requests.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder" /> instance.</param>
        /// <param name="env"><see cref="IWebHostEnvironment" /> instance.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(_configuration.GetSection("CorsOrigin:AllowOrigins").Value);

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