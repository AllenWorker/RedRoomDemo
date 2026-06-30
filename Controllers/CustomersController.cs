using Microsoft.AspNetCore.Mvc;
using RedRoomDemo.Services;

namespace RedRoomDemo.Controllers;

public class CustomersController : Controller
{
    private readonly CustomerService _customerService;

    // This controller no longer contains all business logic.
    // However, it still manually creates CustomerService, so it is tightly coupled to a concrete class.
    public CustomersController()
    {
        // Problem: If CustomerService changes its constructor, every controller that creates it manually must be updated.
        _customerService = new CustomerService();
    }

    public IActionResult Index()
    {
        var model = _customerService.GetCustomers();
        return View(model);
    }
}
