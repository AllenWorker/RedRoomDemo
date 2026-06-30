using RedRoomDemo.Data.Models;
using RedRoomDemo.Models;

namespace RedRoomDemo.Data.Repositories.Interfaces;

public interface IPaymentRepository
{
    Task<int> GetSuccessfulPaymentCountAsync();
    Task<IReadOnlyList<PaymentListItemViewModel>> GetPaymentsAsync();
    Task<IReadOnlyList<PaymentListItemViewModel>> GetUnmatchedSuccessfulPaymentsAsync();
    Task<IReadOnlyList<PaymentTransaction>> GetPaymentsByOrderNumberAsync(string orderNumber);
    Task<IReadOnlyList<UnmatchedSuccessfulPaymentDataModel>> GetUnmatchedSuccessfulPaymentRecordsAsync();
}
