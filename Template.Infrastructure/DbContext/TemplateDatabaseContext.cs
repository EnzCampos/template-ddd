using Microsoft.EntityFrameworkCore;
using Template.Domain.Common;

namespace Template.Infrastructure.DatabaseContext
{
    public class TemplateDatabaseContext(DbContextOptions<TemplateDatabaseContext> options) : DbContext(options)
    {
        // This will automatically set the DtInserted and DtLastUpdate
        // properties of the entities that inherit from BaseEntity it there are any changes
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>()
                .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DtInserted = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.DtLastUpdate = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TemplateDatabaseContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}

