using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using RedRoomDemo.Database;
using RedRoomDemo.Models;

namespace RedRoomDemo.Controllers;

public class PaymentsController : Controller
{
    private readonly IConfiguration _configuration;

    public PaymentsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        // Workshop purpose: intentionally keep query logic, matching decisions,
        // and UI field composition inside the controller.
        using var connection = new SqliteConnection(LegacyDatabaseInitializer.GetConnectionString(_configuration));
        connection.Open();

        const string sql = """
                           SELECT
                               p.TransactionId,
                               p.TransactionReference,
                               p.PaidAmount,
                               p.Status,
                               p.CreatedAt,
                               CASE
                                   WHEN p.TransactionReference IS NULL OR TRIM(p.TransactionReference) = '' THEN 'Missing reference'
                                   WHEN EXISTS (
                                       SELECT 1
                                       FROM Orders o
                                       WHERE o.OrderNumber = p.TransactionReference
                                   ) THEN 'Matched by OrderNumber'
                                   ELSE 'No matching order'
                               END AS MatchHint
                           FROM PaymentTransactions p
                           ORDER BY p.TransactionId DESC;
                           """;

        var payments = connection.Query<PaymentListItemViewModel>(sql).ToList();
        return View(payments);
    }
}
