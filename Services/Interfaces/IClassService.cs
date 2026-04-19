using 프로젝트관리.Models;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Services.Interfaces;

public interface IClassService
{
    Task<List<EducationClass>> GetAllAsync();
    Task<EducationClass?> GetByIdAsync(int id);
    Task CreateAsync(ClassFormViewModel model);
    Task<bool> UpdateAsync(ClassFormViewModel model);
}
