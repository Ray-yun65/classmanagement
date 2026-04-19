using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardViewModel> GetDashboardAsync();
}
