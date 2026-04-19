using System.ComponentModel.DataAnnotations;
using 프로젝트관리.Models;
using 프로젝트관리.Validation;

namespace 프로젝트관리.ViewModels;

[SessionTimeRange]
public class SessionFormViewModel
{
    public int SessionId { get; set; }

    [Display(Name = "교육과정")]
    [Required]
    public int ClassId { get; set; }

    [Display(Name = "세션 일자")]
    [Required]
    public DateOnly SessionDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Display(Name = "시작 시간")]
    [Required]
    public TimeOnly StartTime { get; set; } = new(9, 0);

    [Display(Name = "종료 시간")]
    [Required]
    public TimeOnly EndTime { get; set; } = new(10, 0);

    [Display(Name = "장소")]
    [Required]
    [StringLength(100)]
    public string Location { get; set; } = string.Empty;

    [Display(Name = "상태")]
    [Required]
    public SessionStatus Status { get; set; } = SessionStatus.Scheduled;
}
