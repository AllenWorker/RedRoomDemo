using RedRoomDemo.Data.Repositories.Interfaces;
using RedRoomDemo.Models;
using RedRoomDemo.Services.Interfaces;

namespace RedRoomDemo.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentRepository _paymentRepository;

    public OrderService(IOrderRepository orderRepository, IPaymentRepository paymentRepository)
    {
        _orderRepository = orderRepository;
        _paymentRepository = paymentRepository;
    }

    public Task<IReadOnlyList<OrderListItemViewModel>> GetOrdersAsync()
    {
        // The service layer contains business rules and coordinates data access.
        return _orderRepository.GetOrdersAsync();
    }

    public async Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId)
    {
        var model = await _orderRepository.GetOrderDetailsAsync(orderId);
        if (model is null)
        {
            return null;
        }

        // Keep legacy matching logic intentionally unchanged for workshop storyline.
        model.MatchedPayments = (await _paymentRepository.GetPaymentsByOrderNumberAsync(model.OrderNumber)).ToList();
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

        return model;
    }
}
