using RedRoomDemo.Models;
using RedRoomDemo.Repositories;

namespace RedRoomDemo.Services;

// The service layer now contains business logic.
// However, this service still manually creates concrete repositories.
public class OrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly PaymentRepository _paymentRepository;

    public OrderService()
    {
        // Problem: This service cannot easily switch to another repository implementation for testing or future changes.
        _orderRepository = new OrderRepository();
        _paymentRepository = new PaymentRepository();
    }

    public List<OrderListItemViewModel> GetOrders()
    {
        return _orderRepository.GetOrders();
    }

    public OrderDetailsViewModel? GetOrderDetails(int orderId)
    {
        var model = _orderRepository.GetOrderDetails(orderId);
        if (model is null)
        {
            return null;
        }

        model.MatchedPayments = _paymentRepository.GetPaymentsByOrderNumber(model.OrderNumber);
        model.HasPaymentMatch = model.MatchedPayments.Count > 0;
        model.PaymentMatchNote = GetPaymentMatchNote(model.MatchedPayments.Count);

        return model;
    }

    private static string GetPaymentMatchNote(int matchedPaymentCount)
    {
        return matchedPaymentCount switch
        {
            0 => "No payment matched by TransactionReference. Legacy linkage is unreliable.",
            1 => "Matched exactly one payment record by TransactionReference.",
            _ => $"Matched {matchedPaymentCount} payment records by TransactionReference. Duplicated references were found."
        };
    }
}
