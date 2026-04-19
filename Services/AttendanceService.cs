using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using 프로젝트관리.Data;
using 프로젝트관리.Models;
using 프로젝트관리.Services.Interfaces;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Services;

public class AttendanceService(ApplicationDbContext context) : IAttendanceService
{
    private readonly ApplicationDbContext _context = context;

    public Task<List<Attendance>> GetAllAsync()
        => _context.Attendances
            .Include(a => a.Session)
            .ThenInclude(s => s!.Class)
            .OrderByDescending(a => a.AttendanceId)
            .ToListAsync();

    public Task<Attendance?> GetByIdAsync(int id)
        => _context.Attendances.FirstOrDefaultAsync(a => a.AttendanceId == id);

    public Task<List<SelectListItem>> GetSessionOptionsAsync()
        => _context.Sessions
            .Include(s => s.Class)
            .OrderByDescending(s => s.SessionDate)
            .ThenBy(s => s.StartTime)
            .Select(s => new SelectListItem
            {
                Value = s.SessionId.ToString(),
                Text = $"{s.SessionDate} | {s.Class!.ClassName} | {s.StartTime}"
            })
            .ToListAsync();

    public async Task CreateAsync(AttendanceFormViewModel model)
    {
        var entity = new Attendance
        {
            SessionId = model.SessionId,
            UserName = model.UserName,
            Department = model.Department,
            AttendanceStatus = model.AttendanceStatus
        };
        _context.Attendances.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(AttendanceFormViewModel model)
    {
        var existing = await GetByIdAsync(model.AttendanceId);
        if (existing is null)
        {
            return false;
        }

        existing.SessionId = model.SessionId;
        existing.UserName = model.UserName;
        existing.Department = model.Department;
        existing.AttendanceStatus = model.AttendanceStatus;
        await _context.SaveChangesAsync();
        return true;
    }
}
