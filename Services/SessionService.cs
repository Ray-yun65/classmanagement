using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using 프로젝트관리.Data;
using 프로젝트관리.Models;
using 프로젝트관리.Services.Interfaces;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Services;

public class SessionService(ApplicationDbContext context) : ISessionService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Session>> GetAllAsync(int? classIdFilter)
    {
        var query = _context.Sessions.Include(s => s.Class).AsQueryable();
        if (classIdFilter.HasValue)
        {
            query = query.Where(s => s.ClassId == classIdFilter.Value);
        }

        return await query.OrderByDescending(s => s.SessionDate).ThenBy(s => s.StartTime).ToListAsync();
    }

    public Task<Session?> GetByIdAsync(int id)
        => _context.Sessions.FirstOrDefaultAsync(s => s.SessionId == id);

    public Task<List<SelectListItem>> GetClassOptionsAsync()
        => _context.Classes.OrderBy(c => c.ClassName)
            .Select(c => new SelectListItem { Value = c.ClassId.ToString(), Text = c.ClassName })
            .ToListAsync();

    public async Task CreateAsync(SessionFormViewModel model)
    {
        var entity = new Session
        {
            ClassId = model.ClassId,
            SessionDate = model.SessionDate,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            Location = model.Location,
            Status = model.Status
        };
        _context.Sessions.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(SessionFormViewModel model)
    {
        var existing = await GetByIdAsync(model.SessionId);
        if (existing is null)
        {
            return false;
        }

        existing.ClassId = model.ClassId;
        existing.SessionDate = model.SessionDate;
        existing.StartTime = model.StartTime;
        existing.EndTime = model.EndTime;
        existing.Location = model.Location;
        existing.Status = model.Status;
        await _context.SaveChangesAsync();
        return true;
    }
}
