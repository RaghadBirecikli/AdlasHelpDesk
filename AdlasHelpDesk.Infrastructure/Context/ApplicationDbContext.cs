using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdlasHelpDesk.Domain.Models;
using AdlasHelpDesk.Application.UserHelpers;
using System.ComponentModel;
using AdlasHelpDesk.Application.UserHelpers;
using System.Drawing;
using System.Reflection.Metadata;

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
            modelBuilder.Entity<Member>().HasData(
                 new Member()
                 {
                     Id = Guid.Parse("FD19A058-541E-4D65-B4E0-89F87D354E19"),
                     Name = "Özmen",
                     Surname = "Kızılkoca",
                     IsActive = true,
                     IsAdmin = true,
                     UserName = "sozdijital",
                     Email = "ozmen@sozdijital.com",
                     Password = Functions.MD5("12345"),
                 }
                );
        }

        //newLineWillBeInsertedHereByCodeGeratingTool
        public DbSet<MailParameter> MailParameter { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<SendMail> SendMail { get; set; }

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
        }
        //newLineWillBeInsertedHereByCodeGeratingTool2

        private void ConfigureMailParameter(EntityTypeBuilder<MailParameter> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);
        }

        private void ConfigureCompany(EntityTypeBuilder<Company> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        private void ConfigureMember(EntityTypeBuilder<Member> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Property(x => x.FullName).HasComputedColumnSql("[Name] + ' ' + [Surname]");
        }
        private void ConfigureSendMail(EntityTypeBuilder<SendMail> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
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
