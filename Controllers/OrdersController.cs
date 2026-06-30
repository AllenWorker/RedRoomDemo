using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using RedRoomDemo.Database;
using RedRoomDemo.Models;

namespace RedRoomDemo.Controllers;

public class OrdersController : Controller
{
    private readonly IConfiguration _configuration;

    public OrdersController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        // Workshop purpose: intentionally keep the "fat controller" style,
        // so later branches can demonstrate refactoring.
        using var connection = new SqliteConnection(LegacyDatabaseInitializer.GetConnectionString(_configuration));
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

        var orders = connection.Query<OrderListItemViewModel>(sql).ToList();
        return View(orders);
    }

    public IActionResult Details(int id)
    {
        using var connection = new SqliteConnection(LegacyDatabaseInitializer.GetConnectionString(_configuration));
        connection.Open();

        const string orderSql = """
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

        var model = connection.QueryFirstOrDefault<OrderDetailsViewModel>(orderSql, new { OrderId = id });
        if (model is null)
        {
            return NotFound();
        }

        // Workshop purpose: legacy design flaw — payment matching can only rely on
        // TransactionReference ≈ OrderNumber, which is unreliable.
        const string paymentSql = """
                                  SELECT
                                      TransactionId,
                                      TransactionReference,
                                      PaidAmount,
                                      Status,
                                      CreatedAt
                                  FROM PaymentTransactions
                                  WHERE TransactionReference = @OrderNumber
                                  ORDER BY CreatedAt DESC;
                                  """;

        model.MatchedPayments = connection.Query<PaymentTransaction>(paymentSql, new { model.OrderNumber }).ToList();
        model.HasPaymentMatch = model.MatchedPayments.Count > 0;

        if (!model.HasPaymentMatch)
        {
            model.PaymentMatchNote = "No payment matched by TransactionReference. Legacy linkage is unreliable.";
        }
        else if (model.MatchedPayments.Count == 1)
        {
            model.PaymentMatchNote = "Matched exactly one payment record by TransactionReference.";
        }
        else
        {
            model.PaymentMatchNote = $"Matched {model.MatchedPayments.Count} payment records by TransactionReference. Duplicated references were found.";
        }

        return View(model);
    }
}
