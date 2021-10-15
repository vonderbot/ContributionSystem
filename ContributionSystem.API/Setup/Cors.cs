using Microsoft.AspNetCore.Builder;

namespace ContributionSystem.API.Setup
{
    public static class Cors
    {
        public static void SetCors(this IApplicationBuilder app)
        {
            app.UseCors(cors => cors
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
            );
        }
    }
}
