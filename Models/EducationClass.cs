using System.ComponentModel.DataAnnotations;

namespace 프로젝트관리.Models;

public class EducationClass
{
    [Key]
    public int ClassId { get; set; }

    [Required]
    [StringLength(100)]
    public string ClassName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string TargetDepartment { get; set; } = string.Empty;

    [Required]
    public ClassStatus Status { get; set; } = ClassStatus.Planned;

    [StringLength(1000)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}
