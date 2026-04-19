using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using 프로젝트관리.Models;
using 프로젝트관리.Services.Interfaces;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Pages.Classes;

public class IndexModel(IClassService classService) : PageModel
{
    private readonly IClassService _classService = classService;

    [BindProperty]
    public ClassFormViewModel Input { get; set; } = new();

    public List<EducationClass> Items { get; set; } = new();

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

        await _classService.CreateAsync(Input);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadAsync();
            return Page();
        }

        if (!await _classService.UpdateAsync(Input))
        {
            return RedirectToPage();
        }
        return RedirectToPage();
    }

    private async Task LoadAsync()
    {
        Items = await _classService.GetAllAsync();

        if (EditId.HasValue)
        {
            var editTarget = await _classService.GetByIdAsync(EditId.Value);
            if (editTarget is not null)
            {
                Input = new ClassFormViewModel
                {
                    ClassId = editTarget.ClassId,
                    ClassName = editTarget.ClassName,
                    TargetDepartment = editTarget.TargetDepartment,
                    Status = editTarget.Status,
                    Description = editTarget.Description
                };
            }
        }
    }
}
