using RedRoomDemo.Models;

namespace RedRoomDemo.Data.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task<IReadOnlyList<CustomerListItemViewModel>> GetCustomersAsync();
    Task<int> GetCustomerCountAsync();
}
