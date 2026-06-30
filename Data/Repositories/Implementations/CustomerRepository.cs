using Dapper;
using RedRoomDemo.Data.Database;
using RedRoomDemo.Data.Repositories.Interfaces;
using RedRoomDemo.Models;

namespace RedRoomDemo.Data.Repositories.Implementations;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CustomerRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyList<CustomerListItemViewModel>> GetCustomersAsync()
    {
        // The repository is responsible for SQL and database access.
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

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<CustomerListItemViewModel>(sql);
        return results.ToList();
    }

    public async Task<int> GetCustomerCountAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM Customers;");
    }
}
