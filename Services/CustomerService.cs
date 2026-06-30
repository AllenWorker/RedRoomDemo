using RedRoomDemo.Models;
using RedRoomDemo.Repositories;

namespace RedRoomDemo.Services;

// The service layer now contains business logic.
// However, this service still manually creates a concrete repository.
public class CustomerService
{
    private readonly CustomerRepository _customerRepository;

    public CustomerService()
    {
        // Problem: This service cannot easily switch to another repository implementation for testing or future changes.
        _customerRepository = new CustomerRepository();
    }

    public List<CustomerListItemViewModel> GetCustomers()
    {
        return _customerRepository.GetCustomers();
    }
}
