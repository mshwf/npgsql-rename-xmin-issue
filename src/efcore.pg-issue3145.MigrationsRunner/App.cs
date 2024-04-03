using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Migrations.Context;

namespace MigrationsRunner
{
    public class App
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly ServerDataContext _dbContext;

        #endregion

        #region Constructor

        public App(ILogger<App> logger, ServerDataContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        #endregion

        #region Public Methods

        public void Run()
        {
            _logger.LogInformation("Attempting to run database migrations...");
            _dbContext.Database.Migrate();

            _logger.LogInformation("Database Migrations Completed");
        }

        #endregion
    }
}