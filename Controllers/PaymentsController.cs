using Microsoft.AspNetCore.Mvc;
using RedRoomDemo.Services;

namespace RedRoomDemo.Controllers;

public class PaymentsController : Controller
{
    private readonly PaymentService _paymentService;

    // This controller no longer contains all business logic.
    // However, it still manually creates PaymentService, so it is tightly coupled to a concrete class.
    public PaymentsController()
    {
        // Problem: If PaymentService changes its constructor, every controller that creates it manually must be updated.
        _paymentService = new PaymentService();
    }

    public IActionResult Index()
    {
        var payments = _paymentService.GetPayments();
        return View(payments);
    }
}
