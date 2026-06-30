using Dapper;
using RedRoomDemo.Models;

namespace RedRoomDemo.Repositories;

// The repository is responsible for SQL and database access.
// This is an improvement over putting SQL directly inside controllers.
public class OrderRepository : RepositoryBase
{
    public List<OrderListItemViewModel> GetOrders()
    {
        using var connection = CreateConnection();
        connection.Open();

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

        return connection.Query<OrderListItemViewModel>(sql).ToList();
    }

    public OrderDetailsViewModel? GetOrderDetails(int orderId)
    {
        using var connection = CreateConnection();
        connection.Open();

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

        return connection.QueryFirstOrDefault<OrderDetailsViewModel>(sql, new { OrderId = orderId });
    }

    public int CountOrders()
    {
        using var connection = CreateConnection();
        connection.Open();

        return connection.ExecuteScalar<int>("SELECT COUNT(1) FROM Orders;");
    }

    public HashSet<string> GetOrderNumbers()
    {
        using var connection = CreateConnection();
        connection.Open();

        var orderNumbers = connection.Query<string>("SELECT OrderNumber FROM Orders;");
        return orderNumbers.ToHashSet(StringComparer.OrdinalIgnoreCase);
    }
}
