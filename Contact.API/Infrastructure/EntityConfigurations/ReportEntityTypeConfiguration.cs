using Contact.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact.API.Infrastructure.EntityConfigurations
{
    public class ReportEntityTypeConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Report");

            builder.HasKey(co => co.Id);

            builder.Property(cn => cn.DemandDateUtc)
                .IsRequired();

            builder.Property(cn => cn.Status)
                .IsRequired();
        }
    }
}
