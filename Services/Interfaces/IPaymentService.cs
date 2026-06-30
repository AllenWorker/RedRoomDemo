using RedRoomDemo.Application.DTOs.Payment;
using RedRoomDemo.Models;

namespace RedRoomDemo.Services.Interfaces;

public interface IPaymentService
{
    Task<IReadOnlyList<PaymentListItemViewModel>> GetPaymentsAsync();
    Task<IReadOnlyList<PaymentListItemViewModel>> GetUnmatchedSuccessfulPaymentsAsync();
    Task<IReadOnlyList<PaymentSummaryDto>> GetUnmatchedPaymentSummariesAsync();
}
