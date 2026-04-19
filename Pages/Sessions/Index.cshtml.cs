using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using 프로젝트관리.Models;
using 프로젝트관리.Services.Interfaces;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Pages.Sessions;

public class IndexModel(ISessionService sessionService) : PageModel
{
    private readonly ISessionService _sessionService = sessionService;

    [BindProperty]
    public SessionFormViewModel Input { get; set; } = new();

    public List<Session> Items { get; set; } = new();
    public List<SelectListItem> ClassOptions { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int? EditId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? ClassIdFilter { get; set; }

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

        await _sessionService.CreateAsync(Input);
        return RedirectToPage(new { classIdFilter = Input.ClassId });
    }

    public async Task<IActionResult> OnPostEditAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadAsync();
            return Page();
        }

        if (!await _sessionService.UpdateAsync(Input))
        {
            return RedirectToPage();
        }
        return RedirectToPage(new { classIdFilter = Input.ClassId });
    }

    private async Task LoadAsync()
    {
        ClassOptions = await _sessionService.GetClassOptionsAsync();
        Items = await _sessionService.GetAllAsync(ClassIdFilter);

        if (EditId.HasValue)
        {
            var target = await _sessionService.GetByIdAsync(EditId.Value);
            if (target is not null)
            {
                Input = new SessionFormViewModel
                {
                    SessionId = target.SessionId,
                    ClassId = target.ClassId,
                    SessionDate = target.SessionDate,
                    StartTime = target.StartTime,
                    EndTime = target.EndTime,
                    Location = target.Location,
                    Status = target.Status
                };
            }
        }
        else if (ClassIdFilter.HasValue)
        {
            Input.ClassId = ClassIdFilter.Value;
        }
    }
}
