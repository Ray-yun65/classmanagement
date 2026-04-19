using System.ComponentModel.DataAnnotations;

namespace 프로젝트관리.Models;

public enum AttendanceStatus
{
    [Display(Name = "참석")]
    Attended = 1,
    [Display(Name = "결석")]
    Absent = 2
}
