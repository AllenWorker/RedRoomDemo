using RedRoomDemo.Application.DTOs.Order;
using RedRoomDemo.Models;

namespace RedRoomDemo.Services.Interfaces;

public interface IOrderService
{
    Task<IReadOnlyList<OrderListItemViewModel>> GetOrdersAsync();
    Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId);
    Task<IReadOnlyList<OrderResponseDto>> GetOrderResponsesAsync();
    Task<OrderResponseDto?> GetOrderResponseAsync(int orderId);
}
