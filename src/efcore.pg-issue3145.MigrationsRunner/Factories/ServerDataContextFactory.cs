using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Migrations.Context;

namespace Migrations.Factories
{
    public class ServerDataContextFactory : IDesignTimeDbContextFactory<ServerDataContext>
    {

        public ServerDataContext CreateDbContext(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("migration.appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<ServerDataContext>();
            builder.UseNpgsql(configuration.GetConnectionString("DatabaseServerMigrations"));
            return new ServerDataContext(builder.Options);
        }
    }
}