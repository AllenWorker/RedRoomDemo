using Dapper;
using RedRoomDemo.Models;

namespace RedRoomDemo.Repositories;

// The repository is responsible for SQL and database access.
// This is an improvement over putting SQL directly inside controllers.
public class PaymentRepository : RepositoryBase
{
    public List<PaymentTransaction> GetPayments()
    {
        using var connection = CreateConnection();
        connection.Open();

        const string sql = """
                           SELECT
                               TransactionId,
                               TransactionReference,
                               PaidAmount,
                               Status,
                               CreatedAt
                           FROM PaymentTransactions
                           ORDER BY TransactionId DESC;
                           """;

        return connection.Query<PaymentTransaction>(sql).ToList();
    }

    public List<PaymentTransaction> GetPaymentsByOrderNumber(string orderNumber)
    {
        using var connection = CreateConnection();
        connection.Open();

        const string sql = """
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

        return connection.Query<PaymentTransaction>(sql, new { OrderNumber = orderNumber }).ToList();
    }

    public int CountSuccessfulPayments()
    {
        using var connection = CreateConnection();
        connection.Open();

        return connection.ExecuteScalar<int>(
            "SELECT COUNT(1) FROM PaymentTransactions WHERE Status = 'Success';");
    }
}
