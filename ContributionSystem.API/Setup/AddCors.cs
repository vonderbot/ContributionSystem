using Microsoft.AspNetCore.Builder;

namespace ContributionSystem.API.Setup
{
    public static class AddCors
    {
        public static void Add(IApplicationBuilder app)
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
