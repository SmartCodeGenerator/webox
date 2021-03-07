using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webox.DAL.Entities;

namespace Webox.DAL.Database.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.DeliveryAddress).IsRequired();
            builder.Property(o => o.DeliveryDateTime).IsRequired();
            builder.Property(o => o.PlacementDateTime).IsRequired();
            builder.Property(o => o.Price).IsRequired();

            builder.HasOne(o => o.Account).WithMany(ua => ua.Orders).HasForeignKey(o => o.AccountId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
