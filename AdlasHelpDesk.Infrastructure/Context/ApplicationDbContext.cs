using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdlasHelpDesk.Domain.Models;
using AdlasHelpDesk.Application.UserHelpers;
using System.ComponentModel;
using AdlasHelpDesk.Application.UserHelpers;
using System.Drawing;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace AdlasHelpDesk.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureApplicationDbContext(modelBuilder);
            modelBuilder.Entity<Company>().HasData(
            new Company()
            {

                Id = Guid.Parse("6b6d7d4e-1a23-4a3d-91b2-fc1d32f19491"),
                Name = "Adlas Academy",
                Description = "نؤمن أن التعلم هو مفتاح المستقبل، لذا نركز على توفير أدوات وأساليب حديثة تلهم العقول الشابة وتعدّهم لتحديات العصر الرقمي، منخ لال برامجنا المتقدمة.",
                Email = "Academy@adlas.com",
                Phone = " 011 207 9878",
                Address = " \r\nالرياض 7270 طريق عثمان بن عفان الفرعي\r\n\r\nحي الوادي مكتب رقم 18\r\n\r\nالرمز البريدي - 13313 الرمز الإضافي 4417",
            }
           );
            modelBuilder.Entity<Member>().HasData(
                 new Member()
                 {
                     Id = Guid.Parse("FD19A058-541E-4D65-B4E0-89F87D354E19"),
                     Name = "Raghad",
                     Surname = "Birecikli",
                     IsActive = true,
                     IsAdmin = true,
                     UserName = "Raghad",
                     Email = "Rb@adlas.com",
                     Password = Functions.MD5("12345"),
                     CompanyId=Guid.Parse("6b6d7d4e-1a23-4a3d-91b2-fc1d32f19491"),
                 },
                 new Member()
                 {
                     Id = Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"),
                     Name = "Reem",
                     Surname = "AlBashir",
                     IsActive = true,
                     IsAdmin = false,
                     UserName = "Reem",
                     Email = "Rm@adlas.com",
                     CompanyId = Guid.Parse("6b6d7d4e-1a23-4a3d-91b2-fc1d32f19491"),
                     Password = Functions.MD5("12345"),
                 },
                 new Member()
                 {
                     Id = Guid.Parse("e13a2dfb-5e4d-4a92-8e6f-8347c3f7bcd2"),
                     Name = "Saleh",
                     Surname = "Alshaya",
                     IsActive = true,
                     IsAdmin = false,
                     UserName = "Dr.Saleh",
                     Email = "gm@adlas.com",
                     CompanyId = Guid.Parse("6b6d7d4e-1a23-4a3d-91b2-fc1d32f19491"),
                     Password = Functions.MD5("12345"),
                 }
                );
        }

        //newLineWillBeInsertedHereByCodeGeratingTool
        public DbSet<MailParameter> MailParameter { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<SendMail> SendMail { get; set; }
        public DbSet<AllowedEmailDomain> AllowedEmailDomain { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Library> Library { get; set; }
        public DbSet<ProblemType> ProblemType { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductName> ProductName { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<PurchaseSource> PurchaseSource { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketStatus> TicketStatuse { get; set; }
        public DbSet<University> Universitie { get; set; }

        private void ConfigureApplicationDbContext(ModelBuilder modelBuilder)
        {
            //to get the default value existing in all models annotations
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    var memberInfo = property.PropertyInfo ?? (MemberInfo)property.FieldInfo;
                    if (memberInfo == null) continue;
                    var defaultValue = Attribute.GetCustomAttribute(memberInfo, typeof(DefaultValueAttribute)) as DefaultValueAttribute;
                    if (defaultValue == null) continue;
                    property.SetDefaultValueSql(defaultValue.Value.ToString());
                }
            }
            //to make all forigen keys relationships as Restrict
            var cascadeFKs = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            //newLineWillBeInsertedHereByCodeGeratingTool3

            modelBuilder.Entity<MailParameter>(ConfigureMailParameter);
            modelBuilder.Entity<Company>(ConfigureCompany);
            modelBuilder.Entity<Member>(ConfigureMember);
            modelBuilder.Entity<SendMail>(ConfigureSendMail);
            modelBuilder.Entity<AllowedEmailDomain>(ConfigureAllowedEmailDomain);
            modelBuilder.Entity<Customer>(ConfigureCustomer);
            modelBuilder.Entity<Library>(ConfigureLibrary);
            modelBuilder.Entity<ProblemType>(ConfigureProblemType);
            modelBuilder.Entity<Product>(ConfigureProduct);
            modelBuilder.Entity<ProductName>(ConfigureProductName);
            modelBuilder.Entity<ProductType>(ConfigureProductType);
            modelBuilder.Entity<Publisher>(ConfigurePublisher);
            modelBuilder.Entity<PurchaseSource>(ConfigurePurchaseSource);
            modelBuilder.Entity<Skill>(ConfigureSkill);
            modelBuilder.Entity<Store>(ConfigureStore);
            modelBuilder.Entity<Ticket>(ConfigureTicket);
            modelBuilder.Entity<TicketStatus>(ConfigureTicketStatus);
            modelBuilder.Entity<University>(ConfigureUniversity);
        }
        //newLineWillBeInsertedHereByCodeGeratingTool2
        private void ConfigureMailParameter(EntityTypeBuilder<MailParameter> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        private void ConfigureCompany(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        private void ConfigureMember(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.FullName).HasComputedColumnSql("[Name] + ' ' + [Surname]");
            builder.HasOne(x => x.Company)
                   .WithMany()
                   .HasForeignKey(x => x.CompanyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureSendMail(EntityTypeBuilder<SendMail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        private void ConfigureAllowedEmailDomain(EntityTypeBuilder<AllowedEmailDomain> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        private void ConfigureCustomer(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.University)
                   .WithMany()
                   .HasForeignKey(x => x.UniversityId)
                   .OnDelete(DeleteBehavior.Restrict);

          
        }

        private void ConfigureLibrary(EntityTypeBuilder<Library> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        private void ConfigureProblemType(EntityTypeBuilder<ProblemType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        private void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.ProductType)
                   .WithMany()
                   .HasForeignKey(x => x.ProductTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Publisher)
                   .WithMany()
                   .HasForeignKey(x => x.PublisherId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProductName)
                   .WithMany()
                   .HasForeignKey(x => x.ProductNameId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Skill)
                   .WithMany()
                   .HasForeignKey(x => x.SkillId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureProductName(EntityTypeBuilder<ProductName> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.Publisher)
                   .WithMany()
                   .HasForeignKey(x => x.PublisherId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureProductType(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        private void ConfigurePublisher(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        private void ConfigurePurchaseSource(EntityTypeBuilder<PurchaseSource> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        private void ConfigureSkill(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.Publisher)
                   .WithMany()
                   .HasForeignKey(x => x.PublisherId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureStore(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        private void ConfigureTicket(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        

            builder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProblemType)
                   .WithMany()
                   .HasForeignKey(x => x.ProblemTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PurchaseSource)
                   .WithMany()
                   .HasForeignKey(x => x.PurchaseSourceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.StoreName)
                   .WithMany()
                   .HasForeignKey(x => x.StoreNameId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.LibraryName)
                   .WithMany()
                   .HasForeignKey(x => x.LibraryNameId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.TicketStatus)
                   .WithMany()
                   .HasForeignKey(x => x.TicketStatusId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureTicketStatus(EntityTypeBuilder<TicketStatus> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        private void ConfigureUniversity(EntityTypeBuilder<University> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }
    }
}

//add-migration migrationName => to create new migration
//update-database             => to apply the changes from the last migration to the database on sql
//update-database migrationName     => to apply the changes from the mentioned migration to the database on sql
//remove-migration                  => to remove local migration
// get-migration                    => to list all the created migrations
//script-migration                  => to get the script of all created migrations
//script-migration migrationName    => to get the script of changes from the migration after the one that its name written
//script-migration 1.migrationName 2.migrationName    => to get the script of changes from the migration that after the first mentioned until the second mentiond
//update-database 0                 => to delete all migrations and start from 0 migration
