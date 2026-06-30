using RedRoomDemo.Models;
using RedRoomDemo.Repositories;

namespace RedRoomDemo.Services;

// The service layer now contains business logic.
// However, this service still manually creates concrete repositories.
public class DashboardService
{
    private readonly CustomerRepository _customerRepository;
    private readonly OrderRepository _orderRepository;
    private readonly PaymentRepository _paymentRepository;

    public DashboardService()
    {
        // Problem: This service cannot easily switch to another repository implementation for testing or future changes.
        _customerRepository = new CustomerRepository();
        _orderRepository = new OrderRepository();
        _paymentRepository = new PaymentRepository();
    }

    public DashboardViewModel GetDashboard()
    {
        return new DashboardViewModel
        {
            CustomerCount = _customerRepository.CountCustomers(),
            OrderCount = _orderRepository.CountOrders(),
            SuccessfulPaymentCount = _paymentRepository.CountSuccessfulPayments()
        };
    }
}
