using System.ComponentModel.DataAnnotations;
using 프로젝트관리.Models;

namespace 프로젝트관리.ViewModels;

public class MeetingFormViewModel
{
    public int MeetingId { get; set; }

    [Display(Name = "회의 일자")]
    [Required]
    public DateOnly MeetingDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Display(Name = "회의명")]
    [Required(ErrorMessage = "회의명을 입력하세요.")]
    [StringLength(200)]
    public string MeetingName { get; set; } = string.Empty;

    [Display(Name = "회의 유형")]
    [Required]
    public MeetingType MeetingType { get; set; } = MeetingType.Regular;
}
