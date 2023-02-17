using Contact.API.Entities;
using Contact.API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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

    }

    public class ContactContextDesignFactory : IDesignTimeDbContextFactory<ContactContext>
    {
        
        public ContactContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContactContext>()
                .UseNpgsql();

            return new ContactContext(optionsBuilder.Options);
        }
    }

}
