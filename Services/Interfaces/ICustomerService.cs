using RedRoomDemo.Models;

namespace RedRoomDemo.Services.Interfaces;

public interface ICustomerService
{
    Task<IReadOnlyList<CustomerListItemViewModel>> GetCustomersAsync();
}
