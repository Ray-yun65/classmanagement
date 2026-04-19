using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using 프로젝트관리.Models;
using 프로젝트관리.Services.Interfaces;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Pages.Agendas;

public class IndexModel(IMeetingService meetingService) : PageModel
{
    private readonly IMeetingService _meetingService = meetingService;

    [BindProperty]
    public AgendaFormViewModel Input { get; set; } = new();

    public List<Agenda> Items { get; set; } = new();
    public List<SelectListItem> MeetingOptions { get; set; } = new();
    public List<SelectListItem> ClassOptions { get; set; } = new();
    public List<SelectListItem> SessionOptions { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int? EditId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? MeetingFilterId { get; set; }

    public async Task OnGetAsync() => await LoadAsync();

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (!TryValidateModel(Input, nameof(Input)))
        {
            await LoadAsync();
            return Page();
        }

        await _meetingService.CreateAgendaAsync(Input);
        return RedirectToPage(new { meetingFilterId = Input.MeetingId });
    }

    public async Task<IActionResult> OnPostEditAsync()
    {
        if (!TryValidateModel(Input, nameof(Input)))
        {
            await LoadAsync();
            return Page();
        }

        if (!await _meetingService.UpdateAgendaAsync(Input))
        {
            return RedirectToPage();
        }

        return RedirectToPage(new { meetingFilterId = Input.MeetingId });
    }

    private async Task LoadAsync()
    {
        Items = await _meetingService.GetAgendasAsync(MeetingFilterId);
        MeetingOptions = await _meetingService.GetMeetingOptionsAsync();
        ClassOptions = await _meetingService.GetClassOptionsAsync();
        SessionOptions = await _meetingService.GetSessionOptionsAsync();

        if (EditId.HasValue)
        {
            var agenda = await _meetingService.GetAgendaByIdAsync(EditId.Value);
            if (agenda is not null)
            {
                Input = new AgendaFormViewModel
                {
                    AgendaId = agenda.AgendaId,
                    MeetingId = agenda.MeetingId,
                    Title = agenda.Title,
                    Description = agenda.Description,
                    RelatedClassId = agenda.RelatedClassId,
                    RelatedSessionId = agenda.RelatedSessionId,
                    Requester = agenda.Requester,
                    Owner = agenda.Owner,
                    Priority = agenda.Priority,
                    Status = agenda.Status,
                    Decision = agenda.Decision,
                    ActionItem = agenda.ActionItem,
                    DueDate = agenda.DueDate,
                    IsReflected = agenda.IsReflected
                };
            }
        }
        else if (MeetingFilterId.HasValue)
        {
            Input.MeetingId = MeetingFilterId.Value;
        }
    }
}
