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
            builder.Property(l => l.ModelImagePath).HasDefaultValue("https://firebasestorage.googleapis.com/v0/b/webox-63a97.appspot.com/o/laptops%2Fdefault_laptop_img.png?alt=media&token=74eea36a-2376-4e55-824f-f67a5a4aa753");
        }
    }
}
