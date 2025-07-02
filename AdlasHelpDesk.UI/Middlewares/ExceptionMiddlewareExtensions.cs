using Microsoft.AspNetCore.Builder;
using AdlasHelpDesk.UI.Middleware;

namespace AdlasHelpDesk.UI.Middleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
