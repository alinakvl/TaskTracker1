using DbUp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace TaskTracker.Database;

public class DatabaseInitializer
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(IConfiguration configuration, ILogger<DatabaseInitializer> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public void Initialize()
    {
        // from  appsettings.json 
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        // create db
        EnsureDatabase.For.SqlDatabase(connectionString);

        // DbUp
        var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();
        if (upgrader.IsUpgradeRequired())
        {
            _logger.LogInformation("Found new migrations. Starting upgrade...");

            // migration
            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                _logger.LogError(result.Error, "Migration failed!");

                throw new Exception("Migration failed", result.Error);
            }

            _logger.LogInformation("Database migration successful!");
        }
        else
        {
            _logger.LogInformation("Database is up to date. No migration needed.");
        }
    }
}

