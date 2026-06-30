using Microsoft.Data.Sqlite;
using RedRoomDemo.Database;
using System.Data;

namespace RedRoomDemo.Data.Database;

public class SqliteConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqliteConnectionFactory(IConfiguration configuration)
    {
        _connectionString = LegacyDatabaseInitializer.GetConnectionString(configuration);
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}
