using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace 프로젝트관리.Models;

public class Attendance
{
    [Key]
    public int AttendanceId { get; set; }

    [Required]
    public int SessionId { get; set; }

    [ForeignKey(nameof(SessionId))]
    public Session? Session { get; set; }

    [Required]
    [StringLength(100)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Department { get; set; } = string.Empty;

    [Required]
    public AttendanceStatus AttendanceStatus { get; set; } = AttendanceStatus.Attended;
}
