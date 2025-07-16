using AdlasHelpDesk.Application.Services;
using AdlasHelpDesk.Infrastructure.Managers;
using AdlasHelpDesk.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;

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
            services.AddScoped<IMailParameterRepository, MailParameterRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyService, CompanyService>();

            services.AddScoped<IAllowedEmailDomainRepository, AllowedEmailDomainRepository>();
            services.AddScoped<IAllowedEmailDomainService, AllowedEmailDomainService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<ILibraryService, LibraryService>();

            services.AddScoped<IUniversityRepository, UniversityRepository>();
            services.AddScoped<IUniversityService, UniversityService>();

            services.AddScoped<IProductNameRepository, ProductNameRepository>();
            services.AddScoped<IProductNameService, ProductNameService>();

            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IProductTypeService, ProductTypeService>();


            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();


            services.AddScoped<IProblemTypeRepository, ProblemTypeRepository>();
            services.AddScoped<IProblemTypeService, ProblemTypeService>();


            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IPublisherService, PublisherService>();

            services.AddScoped<IPurchaseSourceRepository, PurchaseSourceRepository>();
            services.AddScoped<IPurchaseSourceService, PurchaseSourceService>();

            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ISkillService, SkillService>();

            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IStoreService, StoreService>();

            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketService, TicketService>();

            services.AddScoped<ITicketStatusRepository, TicketStatusRepository>();
            services.AddScoped<ITicketStatusService, TicketStatusService>();

            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMemberService, MemberService>();

            services.AddScoped<ISendMailRepository, SendMailRepository>();
            services.AddScoped<ISendMailService, SendMailService>();

            //newLineWillBeInsertedHereByCodeGeratingTool


            return services;
        }
    }
}
