using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ContributionSystem.API.Setup
{
    public static class ServiceCollectionCorsOriginsExtension
    {
        public static void SetCors(this IApplicationBuilder app)
        {
            app.UseCors("AllowedOrigins");
        }

        public static void ConfigureCorsForOrigins(this IServiceCollection services, IConfiguration configuration)
        {
            var hosts = configuration.GetSection("AllowedOrigins").Get<List<string>>().ToArray();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowedOrigins", p =>
                {
                    p.WithOrigins(hosts)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }
    }
}
