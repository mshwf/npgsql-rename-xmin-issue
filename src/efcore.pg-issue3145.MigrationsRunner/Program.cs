using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MigrationsRunner.Context;
using System;
using System.IO;

namespace MigrationsRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            using var serviceProvider = Services.BuildServiceProvider();
            serviceProvider.GetRequiredService<App>().Run();
        }

        private static IServiceCollection Services
        {
            get
            {
                var services = new ServiceCollection();

                services.AddDbContext<ServerDataContext>(
                    options => options.UseNpgsql("server=localhost;port=5432;user id=postgres;password=*****;ApplicationName=Iss3145;database=Iss3145;command timeout=0;sslmode=Disable", builder =>
                    {
                        builder.SetPostgresVersion(9, 4);
                    }));

                services.AddSingleton<App>();
                services.AddLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.SetMinimumLevel(LogLevel.Trace);
                });

                return services;
            }
        }
    }
}