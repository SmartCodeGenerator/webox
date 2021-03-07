using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webox.DAL.Entities;

namespace Webox.DAL.Database.EntityConfigurations
{
    public class DelivererConfiguration : IEntityTypeConfiguration<Deliverer>
    {
        public void Configure(EntityTypeBuilder<Deliverer> builder)
        {
            builder.Property(d => d.CompanyName).IsRequired();
            builder.HasIndex(d => d.CompanyName).IsUnique();
            builder.Property(d => d.PhoneNumber).IsRequired();
            builder.HasIndex(d => d.PhoneNumber).IsUnique();
            builder.Property(d => d.MainOfficeAddress).IsRequired();
            builder.HasIndex(d => d.MainOfficeAddress).IsUnique();
        }
    }
}
