using Microsoft.Data.Sqlite;
using RedRoomDemo.Database;

namespace RedRoomDemo.Repositories;

public abstract class RepositoryBase
{
    protected SqliteConnection CreateConnection()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        return new SqliteConnection(LegacyDatabaseInitializer.GetConnectionString(configuration));
    }
}
