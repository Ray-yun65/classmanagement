using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace 프로젝트관리.Models;

public class Session
{
    [Key]
    public int SessionId { get; set; }

    [Required]
    public int ClassId { get; set; }

    [ForeignKey(nameof(ClassId))]
    public EducationClass? Class { get; set; }

    [Required]
    public DateOnly SessionDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Required]
    public TimeOnly StartTime { get; set; } = new(9, 0);

    [Required]
    public TimeOnly EndTime { get; set; } = new(10, 0);

    [Required]
    [StringLength(100)]
    public string Location { get; set; } = string.Empty;

    [Required]
    public SessionStatus Status { get; set; } = SessionStatus.Scheduled;

    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
}
