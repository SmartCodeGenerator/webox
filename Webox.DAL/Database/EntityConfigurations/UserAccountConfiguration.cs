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
            builder.Property(ua => ua.ProfileImagePath).HasDefaultValue("https://firebasestorage.googleapis.com/v0/b/webox-63a97.appspot.com/o/profiles%2Fdefault_profile_img.png?alt=media&token=63549611-0fed-40b6-abde-603778cc63f0");
        }
    }
}
