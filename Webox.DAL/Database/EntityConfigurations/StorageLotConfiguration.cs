using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webox.DAL.Entities;

namespace Webox.DAL.Database.EntityConfigurations
{
    class StorageLotConfiguration : IEntityTypeConfiguration<StorageLot>
    {
        public void Configure(EntityTypeBuilder<StorageLot> builder)
        {
            builder.Property(sl => sl.WarehouseAddress).IsRequired();
            builder.Property(sl => sl.SupplyDateTime).IsRequired();
            builder.Property(sl => sl.LaptopsAmount).IsRequired();
            builder.Property(sl => sl.LaptopsCostPerUnit).IsRequired();

            builder.HasOne(sl => sl.Laptop).WithMany(l => l.StorageLots).HasForeignKey(sl => sl.LaptopId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(sl => sl.Deliverer).WithMany(d => d.StorageLots).HasForeignKey(sl => sl.DelivererId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
