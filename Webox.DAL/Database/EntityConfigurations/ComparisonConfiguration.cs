using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webox.DAL.Entities;

namespace Webox.DAL.Database.EntityConfigurations
{
    public class ComparisonConfiguration : IEntityTypeConfiguration<Comparison>
    {
        public void Configure(EntityTypeBuilder<Comparison> builder)
        {
            builder.HasOne(c => c.Account).WithMany(ua => ua.Comparisons).HasForeignKey(c => c.AccountId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.Laptop).WithMany(l => l.Comparisons).HasForeignKey(c => c.LaptopId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
