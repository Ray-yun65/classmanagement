using Microsoft.AspNetCore.Mvc.RazorPages;
using 프로젝트관리.Services.Interfaces;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDashboardService _dashboardService;

        public IndexModel(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public DashboardViewModel Dashboard { get; set; } = new();

        public async Task OnGetAsync()
        {
            Dashboard = await _dashboardService.GetDashboardAsync();
        }
    }
}
