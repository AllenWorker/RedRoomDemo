using RedRoomDemo.Models;
using RedRoomDemo.Repositories;

namespace RedRoomDemo.Services;

// The service layer now contains business logic.
// However, this service still manually creates concrete repositories.
public class PaymentService
{
    private readonly PaymentRepository _paymentRepository;
    private readonly OrderRepository _orderRepository;

    public PaymentService()
    {
        // Problem: This service cannot easily switch to another repository implementation for testing or future changes.
        _paymentRepository = new PaymentRepository();
        _orderRepository = new OrderRepository();
    }

    public List<PaymentListItemViewModel> GetPayments()
    {
        var orderNumbers = _orderRepository.GetOrderNumbers();

        return _paymentRepository.GetPayments()
            .Select(payment => new PaymentListItemViewModel
            {
                TransactionId = payment.TransactionId,
                TransactionReference = payment.TransactionReference,
                PaidAmount = payment.PaidAmount,
                Status = payment.Status,
                CreatedAt = payment.CreatedAt,
                MatchHint = GetMatchHint(payment.TransactionReference, orderNumbers)
            })
            .ToList();
    }

    private static string GetMatchHint(string? transactionReference, HashSet<string> orderNumbers)
    {
        if (string.IsNullOrWhiteSpace(transactionReference))
        {
            return "Missing reference";
        }

        return orderNumbers.Contains(transactionReference)
            ? "Matched by OrderNumber"
            : "No matching order";
    }
}
