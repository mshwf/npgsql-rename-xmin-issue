using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MigrationsRunner.Entities;
using System.Linq;

namespace MigrationsRunner.Context
{
    public class ServerDataContext : DbContext
    {
        public ServerDataContext(DbContextOptions<ServerDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

#if Net6
            modelBuilder.Model.GetEntityTypes()
              .SelectMany(e => e.GetProperties()).ToList()
              .ForEach(p => p.SetColumnName(p.GetColumnName(StoreObjectIdentifier.Table(p.DeclaringEntityType.GetTableName()!, p.DeclaringEntityType.GetDefaultSchema()!))?.ToLower()));

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.ClrType).UseXminAsConcurrencyToken();
            }

#elif Net8
            modelBuilder.Model.GetEntityTypes()
                            .SelectMany(e => e.GetProperties()).ToList()
                            .ForEach(p => p.SetColumnName(p.GetColumnName(StoreObjectIdentifier.Table(p.DeclaringType.GetTableName()!, 
                            ((IMutableEntityType)p.DeclaringType).GetDefaultSchema()!))?.ToLower()));

#endif
        }

        public DbSet<Setting> Settings { get; set; }

    }
}