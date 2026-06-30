using Microsoft.AspNetCore.Mvc;
using RedRoomDemo.Services.Interfaces;

namespace RedRoomDemo.Controllers.Api;

[ApiController]
[Route("api/payments")]
public class PaymentsApiController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsApiController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("unmatched")]
    public async Task<IActionResult> GetUnmatchedSuccessfulPayments()
    {
        // The API contract remains clean even when the legacy schema is messy.
        var payments = await _paymentService.GetUnmatchedPaymentSummariesAsync();
        return Ok(payments);
    }
}
