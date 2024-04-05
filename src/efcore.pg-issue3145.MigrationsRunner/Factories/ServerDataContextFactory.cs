using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MigrationsRunner.Context;

namespace MigrationsRunner.Factories
{
    public class ServerDataContextFactory : IDesignTimeDbContextFactory<ServerDataContext>
    {

        public ServerDataContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ServerDataContext>();
            builder.UseNpgsql("test");
            return new ServerDataContext(builder.Options);
        }
    }
}