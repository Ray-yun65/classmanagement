using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using 프로젝트관리.Models;
using 프로젝트관리.Services.Interfaces;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Pages.Attendance;

public class IndexModel(IAttendanceService attendanceService) : PageModel
{
    private readonly IAttendanceService _attendanceService = attendanceService;

    [BindProperty]
    public AttendanceFormViewModel Input { get; set; } = new();

    public List<프로젝트관리.Models.Attendance> Items { get; set; } = new();
    public List<SelectListItem> SessionOptions { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int? EditId { get; set; }

    public async Task OnGetAsync()
    {
        await LoadAsync();
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadAsync();
            return Page();
        }

        await _attendanceService.CreateAsync(Input);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadAsync();
            return Page();
        }

        if (!await _attendanceService.UpdateAsync(Input))
        {
            return RedirectToPage();
        }
        return RedirectToPage();
    }

    private async Task LoadAsync()
    {
        SessionOptions = await _attendanceService.GetSessionOptionsAsync();
        Items = await _attendanceService.GetAllAsync();

        if (EditId.HasValue)
        {
            var target = await _attendanceService.GetByIdAsync(EditId.Value);
            if (target is not null)
            {
                Input = new AttendanceFormViewModel
                {
                    AttendanceId = target.AttendanceId,
                    SessionId = target.SessionId,
                    UserName = target.UserName,
                    Department = target.Department,
                    AttendanceStatus = target.AttendanceStatus
                };
            }
        }
    }
}
