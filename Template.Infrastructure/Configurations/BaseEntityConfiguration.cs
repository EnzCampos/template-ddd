using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Common;

namespace Template.Infrastructure.Configurations;
public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.DtInserted)
            .HasColumnName("dtInserted")
            .HasColumnType("datetime")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(e => e.DtLastUpdate)
            .HasColumnName("dtLastUpdate")
            .HasColumnType("datetime");
    }
}
