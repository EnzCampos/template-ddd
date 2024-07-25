using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Models;

namespace Template.Infrastructure.Configurations
{
    public class UserProfileConfiguration : BaseEntityConfiguration<UserProfile>
    {
        public override void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.CoProfile);

            builder.Property(e => e.CoProfile)
                .HasColumnName("coProfile")
                .IsRequired();

            builder.Property(e => e.TxProfile)
                .HasColumnName("txProfile")
                .IsRequired();

            builder.HasData(
                new UserProfile { CoProfile = "ADMN", TxProfile = "Admin", DtInserted = DateTime.Now },
                new UserProfile { CoProfile = "USER", TxProfile = "User", DtInserted = DateTime.Now });
        }
    }
}
