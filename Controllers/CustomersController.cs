using Microsoft.AspNetCore.Mvc;
using RedRoomDemo.Services.Interfaces;

namespace RedRoomDemo.Controllers;

public class CustomersController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<IActionResult> Index()
    {
        // The controller now focuses only on HTTP request handling.
        // Business logic has been moved to the service layer.
        var model = await _customerService.GetCustomersAsync();
        return View(model);
    }
}
