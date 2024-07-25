using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Models;

namespace Template.Infrastructure.Configurations;
public class TemplateUserConfiguration : BaseEntityConfiguration<TemplateUser>
{
    public override void Configure(EntityTypeBuilder<TemplateUser> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.IdUser); // Primary key
        builder.HasAlternateKey(e => e.TxEmail);
        builder.HasAlternateKey(e => e.TxCpf);
        builder.HasAlternateKey(e => e.TxPhone);
        builder.HasAlternateKey(e => e.IdUser);

        builder.Property(e => e.IdUser)
            .HasColumnName("idUser")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.TxName)
            .IsRequired()
            .HasColumnName("txName")
            .HasColumnType("varchar")
            .HasMaxLength(255);

        builder.Property(e => e.TxCpf)
            .IsRequired()
            .HasMaxLength(11)
            .HasColumnName("txCpf")
            .HasColumnType("varchar");

        builder.Property(e => e.TxEmail)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("txEmail")
            .HasColumnType("varchar")
            .HasMaxLength(255);

        builder.Property(e => e.TxPhone)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("txPhone")
            .HasColumnType("varchar")
            .HasMaxLength(20);

        builder.Property(e => e.DtLastLoginDate)
            .HasColumnName("dtLastLoginDate")
            .HasColumnType("datetime");

        builder.Property(e => e.IsActive)
            .HasDefaultValue(true)
            .HasColumnName("isActive");
    }
}
