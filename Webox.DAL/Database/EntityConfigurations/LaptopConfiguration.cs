using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webox.DAL.Entities;

namespace Webox.DAL.Database.EntityConfigurations
{
    public class LaptopConfiguration : IEntityTypeConfiguration<Laptop>
    {
        public void Configure(EntityTypeBuilder<Laptop> builder)
        {
            builder.Property(l => l.ModelName).IsRequired();
            builder.HasIndex(l => l.ModelName).IsUnique();
            builder.Property(l => l.Manufacturer).IsRequired();
            builder.Property(l => l.Processor).IsRequired();
            builder.Property(l => l.GraphicsCard).IsRequired();
            builder.Property(l => l.RAMCapacity).IsRequired();
            builder.Property(l => l.SSDCapacity).IsRequired();
            builder.Property(l => l.ScreenSize).IsRequired();
            builder.Property(l => l.OS).IsRequired();
            builder.Property(l => l.Weight).IsRequired();
            builder.Property(l => l.Price).IsRequired();
            builder.Property(l => l.Rating).IsRequired();
            builder.Property(l => l.IsAvailable).IsRequired();
            builder.Property(l => l.ModelImagePath).IsRequired();
        }
    }
}
