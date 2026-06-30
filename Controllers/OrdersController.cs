using Microsoft.AspNetCore.Mvc;
using RedRoomDemo.Services;

namespace RedRoomDemo.Controllers;

public class OrdersController : Controller
{
    private readonly OrderService _orderService;

    // This controller no longer contains all business logic.
    // However, it still manually creates OrderService, so it is tightly coupled to a concrete class.
    public OrdersController()
    {
        // Problem: If OrderService changes its constructor, every controller that creates it manually must be updated.
        _orderService = new OrderService();
    }

    public IActionResult Index()
    {
        var orders = _orderService.GetOrders();
        return View(orders);
    }

    public IActionResult Details(int id)
    {
        var model = _orderService.GetOrderDetails(id);
        if (model is null)
        {
            return NotFound();
        }

        return View(model);
    }
}
