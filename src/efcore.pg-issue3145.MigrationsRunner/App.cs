using Microsoft.EntityFrameworkCore;
using MigrationsRunner.Context;

namespace MigrationsRunner
{
    internal class App
    {
        private ServerDataContext _dbContext;

        public App(ServerDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        internal void Run()
        {
            _dbContext.Database.Migrate();
        }
    }
}