using Microsoft.AspNetCore.Mvc;
using RedRoomDemo.Services.Interfaces;

namespace RedRoomDemo.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        // The controller now focuses only on HTTP request handling.
        // Business logic has been moved to the service layer.
        var orders = await _orderService.GetOrdersAsync();
        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var model = await _orderService.GetOrderDetailsAsync(id);
        if (model is null)
        {
            return NotFound();
        }

        return View(model);
    }
}
