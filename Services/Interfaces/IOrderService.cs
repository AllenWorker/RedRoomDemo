using RedRoomDemo.Models;

namespace RedRoomDemo.Services.Interfaces;

public interface IOrderService
{
    Task<IReadOnlyList<OrderListItemViewModel>> GetOrdersAsync();
    Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId);
}
