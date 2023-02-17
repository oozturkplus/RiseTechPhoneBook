using Contact.API.Entities;
using Contact.API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Contact.API.Infrastructure
{
    public class ContactContext : DbContext
    {

        public ContactContext(DbContextOptions<ContactContext> options)
            : base(options)
        {

        }

        public DbSet<Person> Person { get; set; } = null!;
        public DbSet<ContactInfo> ContactInfo { get; set; } = null!;

        public DbSet<ContactInfo> Report { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PersonEntityTypeConfiguration());
            builder.ApplyConfiguration(new ReportEntityTypeConfiguration());
            builder.ApplyConfiguration(new ContactInfoEntityTypeConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("RisePhoneBookPostgres");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
