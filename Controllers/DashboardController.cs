using Microsoft.AspNetCore.Mvc;
using RedRoomDemo.Services.Interfaces;

namespace RedRoomDemo.Controllers;

public class DashboardController : Controller
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        // The controller now focuses only on HTTP request handling.
        // Business logic has been moved to the service layer.
        var model = await _dashboardService.GetDashboardAsync();
        return View(model);
    }
}
