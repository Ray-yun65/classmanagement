using Microsoft.EntityFrameworkCore;
using 프로젝트관리.Data;
using 프로젝트관리.Models;
using 프로젝트관리.Services.Interfaces;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Services;

public class ClassService(ApplicationDbContext context) : IClassService
{
    private readonly ApplicationDbContext _context = context;

    public Task<List<EducationClass>> GetAllAsync()
        => _context.Classes.OrderByDescending(c => c.CreatedAt).ToListAsync();

    public Task<EducationClass?> GetByIdAsync(int id)
        => _context.Classes.FirstOrDefaultAsync(c => c.ClassId == id);

    public async Task CreateAsync(ClassFormViewModel model)
    {
        var entity = new EducationClass
        {
            ClassName = model.ClassName,
            TargetDepartment = model.TargetDepartment,
            Status = model.Status,
            Description = model.Description,
            CreatedAt = DateTime.UtcNow
        };

        _context.Classes.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(ClassFormViewModel model)
    {
        var existing = await GetByIdAsync(model.ClassId);
        if (existing is null)
        {
            return false;
        }

        existing.ClassName = model.ClassName;
        existing.TargetDepartment = model.TargetDepartment;
        existing.Status = model.Status;
        existing.Description = model.Description;
        await _context.SaveChangesAsync();
        return true;
    }
}
