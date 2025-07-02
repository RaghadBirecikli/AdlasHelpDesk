using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AdlasHelpDesk.Application.Services;
using AdlasHelpDesk.Infrastructure.Managers;
using AdlasHelpDesk.Infrastructure.Repositories;

namespace AdlasHelpDesk.Infrastructure
{
    public static class InfrastructureContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICurrentUserService, CurrentUserManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();

            //newLineWillBeInsertedHereByCodeGeratingTool
         

            return services;
        }
    }
}
