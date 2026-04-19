using Microsoft.AspNetCore.Mvc.Rendering;
using 프로젝트관리.Models;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Services.Interfaces;

public interface IAttendanceService
{
    Task<List<Attendance>> GetAllAsync();
    Task<Attendance?> GetByIdAsync(int id);
    Task<List<SelectListItem>> GetSessionOptionsAsync();
    Task CreateAsync(AttendanceFormViewModel model);
    Task<bool> UpdateAsync(AttendanceFormViewModel model);
}
