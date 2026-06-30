using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using RedRoomDemo.Database;
using RedRoomDemo.Models;

namespace RedRoomDemo.Controllers;

public class DashboardController : Controller
{
    private readonly IConfiguration _configuration;

    public DashboardController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        // Workshop purpose: intentionally keep SQL and aggregation logic in the controller
        // to demonstrate legacy maintainability pain points.
        using var connection = new SqliteConnection(LegacyDatabaseInitializer.GetConnectionString(_configuration));
        connection.Open();

        var model = new DashboardViewModel
        {
            CustomerCount = connection.ExecuteScalar<int>("SELECT COUNT(1) FROM Customers;"),
            OrderCount = connection.ExecuteScalar<int>("SELECT COUNT(1) FROM Orders;"),
            SuccessfulPaymentCount = connection.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM PaymentTransactions WHERE Status = 'Success';")
        };

        return View(model);
    }
}
