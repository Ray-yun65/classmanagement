using Microsoft.AspNetCore.Mvc.Rendering;
using 프로젝트관리.Models;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Services.Interfaces;

public interface IMeetingService
{
    Task<List<Meeting>> GetMeetingsAsync();
    Task<Meeting?> GetMeetingByIdAsync(int id);
    Task<List<Agenda>> GetAgendasAsync(int? meetingFilterId);
    Task<Agenda?> GetAgendaByIdAsync(int id);
    Task<List<SelectListItem>> GetMeetingOptionsAsync();
    Task<List<SelectListItem>> GetClassOptionsAsync();
    Task<List<SelectListItem>> GetSessionOptionsAsync();
    Task CreateMeetingAsync(MeetingFormViewModel model);
    Task<bool> UpdateMeetingAsync(MeetingFormViewModel model);
    Task CreateAgendaAsync(AgendaFormViewModel model);
    Task<bool> UpdateAgendaAsync(AgendaFormViewModel model);
}
