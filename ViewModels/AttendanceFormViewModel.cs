using System.ComponentModel.DataAnnotations;
using 프로젝트관리.Models;

namespace 프로젝트관리.ViewModels;

public class AttendanceFormViewModel
{
    public int AttendanceId { get; set; }

    [Display(Name = "세션")]
    [Required]
    public int SessionId { get; set; }

    [Display(Name = "참석자")]
    [Required(ErrorMessage = "참석자 이름을 입력하세요.")]
    [StringLength(100)]
    public string UserName { get; set; } = string.Empty;

    [Display(Name = "부서")]
    [Required(ErrorMessage = "부서를 입력하세요.")]
    [StringLength(100)]
    public string Department { get; set; } = string.Empty;

    [Display(Name = "참석 상태")]
    [Required]
    public AttendanceStatus AttendanceStatus { get; set; } = AttendanceStatus.Attended;
}
