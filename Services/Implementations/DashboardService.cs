using RedRoomDemo.Data.Repositories.Interfaces;
using RedRoomDemo.Models;
using RedRoomDemo.Services.Interfaces;

namespace RedRoomDemo.Services.Implementations;

public class DashboardService : IDashboardService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentRepository _paymentRepository;

    public DashboardService(
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        IPaymentRepository paymentRepository)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        // The service layer contains business rules and coordinates data access.
        var customerCount = await _customerRepository.GetCustomerCountAsync();
        var orderCount = await _orderRepository.GetOrderCountAsync();
        var successfulPaymentCount = await _paymentRepository.GetSuccessfulPaymentCountAsync();

        return new DashboardViewModel
        {
            CustomerCount = customerCount,
            OrderCount = orderCount,
            SuccessfulPaymentCount = successfulPaymentCount
        };
    }
}
