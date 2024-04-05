using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MigrationsRunner.Entities;
using System.Linq;

namespace MigrationsRunner.Context
{
    public class ServerDataContext : DbContext
    {
        public ServerDataContext(DbContextOptions<ServerDataContext> options) : base(options) { }

        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Model.GetEntityTypes().ToList().ForEach(t => t.SetTableName(t.GetTableName()?.ToLower()));

            var entityTypes = modelBuilder.Model.GetEntityTypes();
            var entityProperties = entityTypes.SelectMany(e => e.GetProperties()).ToList();

            foreach (var prop in entityProperties)
            {
                // Concurrency tokens are system columns that we shouldn't try to alter their letter-casing
                if (prop.IsConcurrencyToken) continue;
                var colName = prop.GetColumnName(StoreObjectIdentifier.Table(GetTableName(prop)!, GetSchemaName(prop)!));
                prop.SetColumnName(colName?.ToLower());
            }
#if Net6
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.ClrType).UseXminAsConcurrencyToken();
            }
#endif
        }

        private string GetTableName(IMutableProperty mutableProperty)
        {
#if Net6
            return mutableProperty.DeclaringEntityType.GetTableName();
#elif Net8
            return mutableProperty.DeclaringType.GetTableName();
#endif
        }

        private string GetSchemaName(IMutableProperty mutableProperty)
        {
#if Net6
            return mutableProperty.DeclaringEntityType.GetDefaultSchema();
#elif Net8
            return ((IMutableEntityType)mutableProperty.DeclaringType).GetDefaultSchema();
#endif
        }
    }
}