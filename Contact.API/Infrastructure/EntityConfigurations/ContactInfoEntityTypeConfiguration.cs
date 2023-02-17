using Contact.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Contact.API.Infrastructure.EntityConfigurations
{
    class ContactInfoEntityTypeConfiguration : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.ToTable("ContactInfo");

            builder.HasKey(ci => ci.Id);


            builder.Property(ci => ci.ContactInfoTypeId)
                .IsRequired();
            builder.Property(ci => ci.Info)
            .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(ci=> ci.Person)
            .WithMany(ci=>ci.ContactInfos)
            .HasForeignKey(p => p.PersonId);
        }
    }
}
