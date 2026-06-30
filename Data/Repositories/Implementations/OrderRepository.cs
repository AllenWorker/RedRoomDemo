using Dapper;
using RedRoomDemo.Data.Database;
using RedRoomDemo.Data.Repositories.Interfaces;
using RedRoomDemo.Models;

namespace RedRoomDemo.Data.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public OrderRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyList<OrderListItemViewModel>> GetOrdersAsync()
    {
        // The repository is responsible for SQL and database access.
        const string sql = """
                           SELECT
                               o.OrderId,
                               o.OrderNumber,
                               c.Name AS CustomerName,
                               o.TotalAmount,
                               o.Status
                           FROM Orders o
                           INNER JOIN Customers c ON c.CustomerId = o.CustomerId
                           ORDER BY o.OrderId DESC;
                           """;

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<OrderListItemViewModel>(sql);
        return results.ToList();
    }

    public async Task<int> GetOrderCountAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM Orders;");
    }

    public async Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId)
    {
        const string sql = """
                           SELECT
                               o.OrderId,
                               o.OrderNumber,
                               o.TotalAmount,
                               o.Status AS OrderStatus,
                               c.Name AS CustomerName,
                               c.Email AS CustomerEmail
                           FROM Orders o
                           INNER JOIN Customers c ON c.CustomerId = o.CustomerId
                           WHERE o.OrderId = @OrderId;
                           """;

        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<OrderDetailsViewModel>(sql, new { OrderId = orderId });
    }
}
