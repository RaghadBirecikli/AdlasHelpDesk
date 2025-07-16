
using AutoMapper;
using AdlasHelpDesk.Application.Results;
using AdlasHelpDesk.Domain.Models;

namespace AdlasHelpDesk.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //newLineWillBeInsertedHereByCodeGeratingTool
            CreateMap<SendMail, SendMailUpsertDto>().ReverseMap();

            CreateMap<University, UniversityDto>().ReverseMap();
            CreateMap<University, UniversityUpsertDto>().ReverseMap();  

             CreateMap<Library, LibraryDto>().ReverseMap();
            CreateMap<Library, LibraryUpsertDto>().ReverseMap();  
            
            CreateMap<AllowedEmailDomain, AllowedEmailDomainDto>().ReverseMap();
            CreateMap<AllowedEmailDomain, AllowedEmailDomainUpsertDto>().ReverseMap();

            // Company
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Company, CompanyUpsertDto>().ReverseMap();

            // Member
            CreateMap<Member, MemberDto>().ReverseMap();
            CreateMap<Member, MemberUpsertDto>().ReverseMap();

            // Customer
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerUpsertDto>().ReverseMap();

            // ProblemType
            CreateMap<ProblemType, ProblemTypeDto>().ReverseMap();
            CreateMap<ProblemType, ProblemTypeUpsertDto>().ReverseMap();

            // Product
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductUpsertDto>().ReverseMap();

            // ProductName
            CreateMap<ProductName, ProductNameDto>().ReverseMap();
            CreateMap<ProductName, ProductNameUpsertDto>().ReverseMap();

            // ProductType
            CreateMap<ProductType, ProductTypeDto>().ReverseMap();
            CreateMap<ProductType, ProductTypeUpsertDto>().ReverseMap();

            // Publisher
            CreateMap<Publisher, PublisherDto>().ReverseMap();
            CreateMap<Publisher, PublisherUpsertDto>().ReverseMap();

            // PurchaseSource
            CreateMap<PurchaseSource, PurchaseSourceDto>().ReverseMap();
            CreateMap<PurchaseSource, PurchaseSourceUpsertDto>().ReverseMap();

            // Store
            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<Store, StoreUpsertDto>().ReverseMap();

            // TicketStatus
            CreateMap<TicketStatus, TicketStatusDto>().ReverseMap();
            CreateMap<TicketStatus, TicketStatusUpsertDto>().ReverseMap();
            
            // Ticket
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<Ticket, TicketUpsertDto>().ReverseMap();

            // Skill
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<Skill, SkillUpsertDto>().ReverseMap();

        }
    }
}
