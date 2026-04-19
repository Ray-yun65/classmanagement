using Microsoft.AspNetCore.Mvc.Rendering;
using 프로젝트관리.Models;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Services.Interfaces;

public interface ISessionService
{
    Task<List<Session>> GetAllAsync(int? classIdFilter);
    Task<Session?> GetByIdAsync(int id);
    Task<List<SelectListItem>> GetClassOptionsAsync();
    Task CreateAsync(SessionFormViewModel model);
    Task<bool> UpdateAsync(SessionFormViewModel model);
}
