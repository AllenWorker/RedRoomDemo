using Dapper;
using Microsoft.Data.Sqlite;

namespace RedRoomDemo.Database;

public static class LegacyDatabaseInitializer
{
    public static void Initialize(IConfiguration configuration)
    {
        var connectionString = GetConnectionString(configuration);

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        // Workshop purpose: create tables and seed data during application startup,
        // intentionally simulating a common but suboptimal legacy approach.
        CreateTables(connection);
        SeedData(connection);
    }

    public static string GetConnectionString(IConfiguration configuration)
    {
        var configured = configuration.GetConnectionString("LegacyDb");
        if (!string.IsNullOrWhiteSpace(configured))
        {
            return configured;
        }

        var dbFolder = Path.Combine(AppContext.BaseDirectory, "Database");
        Directory.CreateDirectory(dbFolder);

        var dbPath = Path.Combine(dbFolder, "legacy-enterprise.db");
        return $"Data Source={dbPath}";
    }

    private static void CreateTables(SqliteConnection connection)
    {
        const string sql = """
                           CREATE TABLE IF NOT EXISTS Customers
                           (
                               CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
                               Name TEXT NOT NULL,
                               Email TEXT
                           );

                           CREATE TABLE IF NOT EXISTS Orders
                           (
                               OrderId INTEGER PRIMARY KEY AUTOINCREMENT,
                               OrderNumber TEXT NOT NULL,
                               CustomerId INTEGER NOT NULL,
                               TotalAmount REAL NOT NULL,
                               Status TEXT NOT NULL
                           );

                           CREATE TABLE IF NOT EXISTS PaymentTransactions
                           (
                               TransactionId INTEGER PRIMARY KEY AUTOINCREMENT,
                               TransactionReference TEXT,
                               PaidAmount REAL NOT NULL,
                               Status TEXT NOT NULL,
                               CreatedAt TEXT NOT NULL
                           );
                           """;

        connection.Execute(sql);
    }

    private static void SeedData(SqliteConnection connection)
    {
        var customerCount = connection.ExecuteScalar<int>("SELECT COUNT(1) FROM Customers;");
        if (customerCount > 0)
        {
            return;
        }

        var random = new Random(2026);

        var customers = new List<(string Name, string Email)>();
        for (var i = 1; i <= 20; i++)
        {
            customers.Add(($"Customer {i:00}", $"customer{i:00}@legacycorp.local"));
        }

        foreach (var customer in customers)
        {
            connection.Execute(
                "INSERT INTO Customers (Name, Email) VALUES (@Name, @Email);",
                new { customer.Name, customer.Email });
        }

        var orderNumbers = new List<string>();
        var statuses = new[] { "Pending", "Paid", "Cancelled", "Processing", "OnHold" };

        for (var i = 1; i <= 100; i++)
        {
            var orderNumber = $"ORD-2026-{i:0000}";
            orderNumbers.Add(orderNumber);

            connection.Execute(
                """
                INSERT INTO Orders (OrderNumber, CustomerId, TotalAmount, Status)
                VALUES (@OrderNumber, @CustomerId, @TotalAmount, @Status);
                """,
                new
                {
                    OrderNumber = orderNumber,
                    CustomerId = random.Next(1, 21),
                    TotalAmount = Math.Round((decimal)(random.NextDouble() * 4500 + 300), 2),
                    Status = statuses[random.Next(statuses.Length)]
                });
        }

        var paymentStatuses = new[] { "Success", "Failed", "Pending", "Reversed" };
        var createdAtBase = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc);

        for (var i = 1; i <= 120; i++)
        {
            string? transactionReference;

            if (i <= 80)
            {
                transactionReference = orderNumbers[random.Next(orderNumbers.Count)];
            }
            else if (i <= 100)
            {
                transactionReference = $"LEGACY-UNKNOWN-{i:000}";
            }
            else if (i <= 110)
            {
                transactionReference = i % 2 == 0 ? string.Empty : null;
            }
            else
            {
                // Intentionally create duplicated references to simulate legacy integration flaws.
                transactionReference = orderNumbers[(i - 111) % 10];
            }

            connection.Execute(
                """
                INSERT INTO PaymentTransactions (TransactionReference, PaidAmount, Status, CreatedAt)
                VALUES (@TransactionReference, @PaidAmount, @Status, @CreatedAt);
                """,
                new
                {
                    TransactionReference = transactionReference,
                    PaidAmount = Math.Round((decimal)(random.NextDouble() * 4200 + 150), 2),
                    Status = paymentStatuses[random.Next(paymentStatuses.Length)],
                    CreatedAt = createdAtBase.AddHours(i * 5).ToString("yyyy-MM-dd HH:mm:ss")
                });
        }
    }
}
