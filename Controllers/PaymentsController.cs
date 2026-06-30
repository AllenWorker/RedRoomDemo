using Microsoft.AspNetCore.Mvc;
using RedRoomDemo.Services.Interfaces;

namespace RedRoomDemo.Controllers;

public class PaymentsController : Controller
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task<IActionResult> Index()
    {
        // The controller now focuses only on HTTP request handling.
        // Business logic has been moved to the service layer.
        var payments = await _paymentService.GetPaymentsAsync();
        return View(payments);
    }

    public async Task<IActionResult> UnmatchedSuccessful()
    {
        var payments = await _paymentService.GetUnmatchedSuccessfulPaymentsAsync();
        return View(payments);
    }
}
