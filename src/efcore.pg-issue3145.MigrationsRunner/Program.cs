using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Migrations.Context;

namespace MigrationsRunner
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        #region Main

        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            var services = ConfigureServices();

            using var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<App>().Run();
        }

        #endregion

        #region Private Methods

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config);

            services.AddDbContext<ServerDataContext>(
                options => options.UseNpgsql(config.GetConnectionString("DatabaseServerMigrations"), builder =>
                {
                    builder.MigrationsAssembly("efcore.pg-issue3145.Migrations");
                    builder.MigrationsHistoryTable("migrations_history");
                    builder.SetPostgresVersion(9, 4);
                }));


            services.AddSingleton<App>();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                // Trace log level is then overridden by the appsettings.json content
                builder.SetMinimumLevel(LogLevel.Trace);
            });

            return services;
        }

        private static IConfiguration LoadConfiguration()
        {
            var processModule = Process.GetCurrentProcess().MainModule;
            var pathToExe = processModule?.FileName;
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);
            var builder = new ConfigurationBuilder().SetBasePath(pathToContentRoot);

            return builder.Build();
        }

        #endregion
    }
}