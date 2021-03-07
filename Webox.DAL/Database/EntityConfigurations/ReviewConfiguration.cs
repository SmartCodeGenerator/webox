using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webox.DAL.Entities;

namespace Webox.DAL.Database.EntityConfigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(r => r.PublishDateTime).IsRequired();
            builder.Property(r => r.Rating).IsRequired();

            builder.HasOne(r => r.Account).WithMany(ua => ua.Reviews).HasForeignKey(r => r.AccountId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(r => r.Laptop).WithMany(l => l.Reviews).HasForeignKey(r => r.LaptopId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
