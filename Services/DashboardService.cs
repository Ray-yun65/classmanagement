using Microsoft.EntityFrameworkCore;
using 프로젝트관리.Data;
using 프로젝트관리.Models;
using 프로젝트관리.Services.Interfaces;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Services;

public class DashboardService(ApplicationDbContext context) : IDashboardService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        return new DashboardViewModel
        {
            TotalClasses = await _context.Classes.CountAsync(),
            PlannedClasses = await _context.Classes.CountAsync(c => c.Status == ClassStatus.Planned),
            OngoingClasses = await _context.Classes.CountAsync(c => c.Status == ClassStatus.Ongoing),
            CompletedClasses = await _context.Classes.CountAsync(c => c.Status == ClassStatus.Completed),
            OpenAgendas = await _context.Agendas.CountAsync(a => a.Status == AgendaStatus.Open),
            DecidedAgendas = await _context.Agendas.CountAsync(a => a.Status == AgendaStatus.Decided),
            OnHoldAgendas = await _context.Agendas.CountAsync(a => a.Status == AgendaStatus.OnHold),
            OverdueActionItems = await _context.Agendas.CountAsync(a => a.DueDate.HasValue && a.DueDate < today && !a.IsReflected)
        };
    }
}
