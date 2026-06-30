using RedRoomDemo.Models;

namespace RedRoomDemo.Data.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<IReadOnlyList<OrderListItemViewModel>> GetOrdersAsync();
    Task<int> GetOrderCountAsync();
    Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId);
}
