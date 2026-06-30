using Dapper;
using RedRoomDemo.Models;

namespace RedRoomDemo.Repositories;

// The repository is responsible for SQL and database access.
// This is an improvement over putting SQL directly inside controllers.
public class CustomerRepository : RepositoryBase
{
    public List<CustomerListItemViewModel> GetCustomers()
    {
        using var connection = CreateConnection();
        connection.Open();

        const string sql = """
                           SELECT
                               c.CustomerId,
                               c.Name,
                               c.Email,
                               COUNT(o.OrderId) AS OrderCount,
                               COALESCE(SUM(o.TotalAmount), 0) AS TotalOrderAmount
                           FROM Customers c
                           LEFT JOIN Orders o ON o.CustomerId = c.CustomerId
                           GROUP BY c.CustomerId, c.Name, c.Email
                           ORDER BY c.CustomerId;
                           """;

        return connection.Query<CustomerListItemViewModel>(sql).ToList();
    }

    public int CountCustomers()
    {
        using var connection = CreateConnection();
        connection.Open();

        return connection.ExecuteScalar<int>("SELECT COUNT(1) FROM Customers;");
    }
}
