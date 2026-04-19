using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using 프로젝트관리.Models;
using 프로젝트관리.Services.Interfaces;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Pages.Meetings;

public class IndexModel(IMeetingService meetingService) : PageModel
{
    private readonly IMeetingService _meetingService = meetingService;

    [BindProperty]
    public MeetingFormViewModel MeetingInput { get; set; } = new();

    public List<Meeting> Meetings { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int? EditMeetingId { get; set; }

    public async Task OnGetAsync()
    {
        await LoadAsync();
    }

    public async Task<IActionResult> OnPostCreateMeetingAsync()
    {
        if (!TryValidateModel(MeetingInput, nameof(MeetingInput)))
        {
            await LoadAsync();
            return Page();
        }

        await _meetingService.CreateMeetingAsync(MeetingInput);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditMeetingAsync()
    {
        if (!TryValidateModel(MeetingInput, nameof(MeetingInput)))
        {
            await LoadAsync();
            return Page();
        }

        if (!await _meetingService.UpdateMeetingAsync(MeetingInput))
        {
            return RedirectToPage();
        }
        return RedirectToPage();
    }

    private async Task LoadAsync()
    {
        Meetings = await _meetingService.GetMeetingsAsync();

        if (EditMeetingId.HasValue)
        {
            var meeting = await _meetingService.GetMeetingByIdAsync(EditMeetingId.Value);
            if (meeting is not null)
            {
                MeetingInput = new MeetingFormViewModel
                {
                    MeetingId = meeting.MeetingId,
                    MeetingDate = meeting.MeetingDate,
                    MeetingName = meeting.MeetingName,
                    MeetingType = meeting.MeetingType
                };
            }
        }
    }
}
