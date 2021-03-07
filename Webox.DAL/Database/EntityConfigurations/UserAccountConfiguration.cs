using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webox.DAL.Entities;

namespace Webox.DAL.Database.EntityConfigurations
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.Property(ua => ua.FirstName).IsRequired();
            builder.Property(ua => ua.LastName).IsRequired();
            builder.Property(ua => ua.Email).IsRequired();
            builder.HasIndex(ua => ua.Email).IsUnique();
            builder.Property(ua => ua.Password).IsRequired();
            builder.Property(ua => ua.IsEmployee).IsRequired();
        }
    }
}
