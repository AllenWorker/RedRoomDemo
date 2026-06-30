using Dapper;
using RedRoomDemo.Data.Database;
using RedRoomDemo.Data.Models;
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

    public async Task<IReadOnlyList<OrderWithPaymentDataModel>> GetOrdersWithPaymentStatusAsync()
    {
        // The database cannot be changed, so the application adapts in SQL queries.
        const string sql = """
                           SELECT
                               o.OrderId,
                               o.OrderNumber,
                               c.Name AS CustomerName,
                               o.TotalAmount,
                               p.Status AS PaymentStatus,
                               CASE WHEN p.TransactionId IS NULL THEN 0 ELSE 1 END AS HasMatchedPaymentInt
                           FROM Orders o
                           INNER JOIN Customers c ON c.CustomerId = o.CustomerId
                           LEFT JOIN PaymentTransactions p ON p.TransactionId =
                           (
                               SELECT p2.TransactionId
                               FROM PaymentTransactions p2
                               WHERE p2.TransactionReference = o.OrderNumber
                               ORDER BY p2.CreatedAt DESC, p2.TransactionId DESC
                               LIMIT 1
                           )
                           ORDER BY o.OrderId DESC;
                           """;

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<OrderWithPaymentDataModel>(sql);
        return results.ToList();
    }

    public async Task<OrderWithPaymentDataModel?> GetOrderWithPaymentStatusAsync(int orderId)
    {
        const string sql = """
                           SELECT
                               o.OrderId,
                               o.OrderNumber,
                               c.Name AS CustomerName,
                               o.TotalAmount,
                               p.Status AS PaymentStatus,
                               CASE WHEN p.TransactionId IS NULL THEN 0 ELSE 1 END AS HasMatchedPaymentInt
                           FROM Orders o
                           INNER JOIN Customers c ON c.CustomerId = o.CustomerId
                           LEFT JOIN PaymentTransactions p ON p.TransactionId =
                           (
                               SELECT p2.TransactionId
                               FROM PaymentTransactions p2
                               WHERE p2.TransactionReference = o.OrderNumber
                               ORDER BY p2.CreatedAt DESC, p2.TransactionId DESC
                               LIMIT 1
                           )
                           WHERE o.OrderId = @OrderId;
                           """;

        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<OrderWithPaymentDataModel>(sql, new { OrderId = orderId });
    }
}
