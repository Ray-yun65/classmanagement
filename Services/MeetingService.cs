using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using 프로젝트관리.Data;
using 프로젝트관리.Models;
using 프로젝트관리.Services.Interfaces;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Services;

public class MeetingService(ApplicationDbContext context) : IMeetingService
{
    private readonly ApplicationDbContext _context = context;

    public Task<List<Meeting>> GetMeetingsAsync()
        => _context.Meetings.OrderByDescending(m => m.MeetingDate).ToListAsync();

    public Task<Meeting?> GetMeetingByIdAsync(int id)
        => _context.Meetings.FirstOrDefaultAsync(m => m.MeetingId == id);

    public async Task<List<Agenda>> GetAgendasAsync(int? meetingFilterId)
    {
        var query = _context.Agendas
            .Include(a => a.Meeting)
            .Include(a => a.RelatedClass)
            .Include(a => a.RelatedSession)
            .AsQueryable();

        if (meetingFilterId.HasValue)
        {
            query = query.Where(a => a.MeetingId == meetingFilterId.Value);
        }

        return await query.OrderByDescending(a => a.DueDate).ThenBy(a => a.Priority).ToListAsync();
    }

    public Task<Agenda?> GetAgendaByIdAsync(int id)
        => _context.Agendas.FirstOrDefaultAsync(a => a.AgendaId == id);

    public async Task<List<SelectListItem>> GetMeetingOptionsAsync()
        => (await GetMeetingsAsync())
            .Select(m => new SelectListItem
            {
                Value = m.MeetingId.ToString(),
                Text = $"{m.MeetingDate} | {m.MeetingName}"
            }).ToList();

    public Task<List<SelectListItem>> GetClassOptionsAsync()
        => _context.Classes.OrderBy(c => c.ClassName)
            .Select(c => new SelectListItem { Value = c.ClassId.ToString(), Text = c.ClassName })
            .ToListAsync();

    public Task<List<SelectListItem>> GetSessionOptionsAsync()
        => _context.Sessions.OrderByDescending(s => s.SessionDate)
            .Select(s => new SelectListItem { Value = s.SessionId.ToString(), Text = $"{s.SessionDate} | {s.Location}" })
            .ToListAsync();

    public async Task CreateMeetingAsync(MeetingFormViewModel model)
    {
        var entity = new Meeting
        {
            MeetingDate = model.MeetingDate,
            MeetingName = model.MeetingName,
            MeetingType = model.MeetingType
        };
        _context.Meetings.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateMeetingAsync(MeetingFormViewModel model)
    {
        var existing = await GetMeetingByIdAsync(model.MeetingId);
        if (existing is null)
        {
            return false;
        }

        existing.MeetingDate = model.MeetingDate;
        existing.MeetingName = model.MeetingName;
        existing.MeetingType = model.MeetingType;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task CreateAgendaAsync(AgendaFormViewModel model)
    {
        var entity = new Agenda
        {
            MeetingId = model.MeetingId,
            Title = model.Title,
            Description = model.Description,
            RelatedClassId = model.RelatedClassId,
            RelatedSessionId = model.RelatedSessionId,
            Requester = model.Requester,
            Owner = model.Owner,
            Priority = model.Priority,
            Status = model.Status,
            Decision = model.Decision,
            ActionItem = model.ActionItem,
            DueDate = model.DueDate,
            IsReflected = model.IsReflected
        };
        _context.Agendas.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAgendaAsync(AgendaFormViewModel model)
    {
        var existing = await GetAgendaByIdAsync(model.AgendaId);
        if (existing is null)
        {
            return false;
        }

        existing.MeetingId = model.MeetingId;
        existing.Title = model.Title;
        existing.Description = model.Description;
        existing.RelatedClassId = model.RelatedClassId;
        existing.RelatedSessionId = model.RelatedSessionId;
        existing.Requester = model.Requester;
        existing.Owner = model.Owner;
        existing.Priority = model.Priority;
        existing.Status = model.Status;
        existing.Decision = model.Decision;
        existing.ActionItem = model.ActionItem;
        existing.DueDate = model.DueDate;
        existing.IsReflected = model.IsReflected;
        await _context.SaveChangesAsync();
        return true;
    }
}
