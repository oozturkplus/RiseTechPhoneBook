using Contact.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact.API.Infrastructure.EntityConfigurations
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(co => co.Id);

            builder.Property(cn => cn.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cn => cn.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cn => cn.Company)
                .HasMaxLength(70);
        }
    }
}
