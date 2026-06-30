using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using RedRoomDemo.Database;
using RedRoomDemo.Models;

namespace RedRoomDemo.Controllers;

public class CustomersController : Controller
{
    private readonly IConfiguration _configuration;

    public CustomersController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        // Workshop purpose: intentionally keep SQL, mapping, and view model assembly in the controller.
        using var connection = new SqliteConnection(LegacyDatabaseInitializer.GetConnectionString(_configuration));
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

        var model = connection.Query<CustomerListItemViewModel>(sql).ToList();
        return View(model);
    }
}
