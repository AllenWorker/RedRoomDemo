using System.Data;

namespace RedRoomDemo.Data.Database;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
