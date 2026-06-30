using Microsoft.AspNetCore.Mvc;
using RedRoomDemo.Services;

namespace RedRoomDemo.Controllers;

public class DashboardController : Controller
{
    private readonly DashboardService _dashboardService;

    // This controller no longer contains all business logic.
    // However, it still manually creates DashboardService, so it is tightly coupled to a concrete class.
    public DashboardController()
    {
        // Problem: If DashboardService changes its constructor, every controller that creates it manually must be updated.
        _dashboardService = new DashboardService();
    }

    public IActionResult Index()
    {
        var model = _dashboardService.GetDashboard();
        return View(model);
    }
}
