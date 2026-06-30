using Dapper;
using RedRoomDemo.Data.Database;
using RedRoomDemo.Data.Repositories.Interfaces;
using RedRoomDemo.Models;

namespace RedRoomDemo.Data.Repositories.Implementations;

public class PaymentRepository : IPaymentRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public PaymentRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> GetSuccessfulPaymentCountAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(1) FROM PaymentTransactions WHERE Status = 'Success';");
    }

    public async Task<IReadOnlyList<PaymentListItemViewModel>> GetPaymentsAsync()
    {
        // The repository is responsible for SQL and database access.
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

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<PaymentListItemViewModel>(sql);
        return results.ToList();
    }

    public async Task<IReadOnlyList<PaymentListItemViewModel>> GetUnmatchedSuccessfulPaymentsAsync()
    {
        const string sql = """
                           SELECT
                               p.TransactionId,
                               p.TransactionReference,
                               p.PaidAmount,
                               p.Status,
                               p.CreatedAt,
                               'No matching order found' AS MatchHint
                           FROM PaymentTransactions p
                           WHERE p.Status = 'Success'
                             AND NOT EXISTS
                             (
                                 SELECT 1
                                 FROM Orders o
                                 WHERE o.OrderNumber = p.TransactionReference
                             )
                           ORDER BY p.TransactionId DESC;
                           """;

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<PaymentListItemViewModel>(sql);
        return results.ToList();
    }

    public async Task<IReadOnlyList<PaymentTransaction>> GetPaymentsByOrderNumberAsync(string orderNumber)
    {
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

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<PaymentTransaction>(sql, new { OrderNumber = orderNumber });
        return results.ToList();
    }
}
