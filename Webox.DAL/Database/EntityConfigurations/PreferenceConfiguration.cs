using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webox.DAL.Entities;

namespace Webox.DAL.Database.EntityConfigurations
{
    public class PreferenceConfiguration : IEntityTypeConfiguration<Preference>
    {
        public void Configure(EntityTypeBuilder<Preference> builder)
        {
            builder.HasOne(p => p.Account).WithMany(ua => ua.Preferences).HasForeignKey(p => p.AccountId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Laptop).WithMany(l => l.Preferences).HasForeignKey(p => p.LaptopId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
