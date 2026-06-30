using RedRoomDemo.Data.Repositories.Interfaces;
using RedRoomDemo.Models;
using RedRoomDemo.Services.Interfaces;

namespace RedRoomDemo.Services.Implementations;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public Task<IReadOnlyList<PaymentListItemViewModel>> GetPaymentsAsync()
    {
        // The service layer contains business rules and coordinates data access.
        return _paymentRepository.GetPaymentsAsync();
    }

    public Task<IReadOnlyList<PaymentListItemViewModel>> GetUnmatchedSuccessfulPaymentsAsync()
    {
        return _paymentRepository.GetUnmatchedSuccessfulPaymentsAsync();
    }
}
