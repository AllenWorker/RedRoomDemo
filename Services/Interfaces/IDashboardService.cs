using RedRoomDemo.Models;

namespace RedRoomDemo.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardViewModel> GetDashboardAsync();
}
