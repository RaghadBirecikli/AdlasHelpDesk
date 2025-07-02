using Microsoft.Extensions.DependencyInjection;

namespace AdlasHelpDesk.Application
{
    public static class ApplicationContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
