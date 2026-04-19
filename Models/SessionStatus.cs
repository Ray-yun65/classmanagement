using System.ComponentModel.DataAnnotations;

namespace 프로젝트관리.Models;

public enum SessionStatus
{
    [Display(Name = "예정")]
    Scheduled = 1,
    [Display(Name = "완료")]
    Completed = 2,
    [Display(Name = "취소")]
    Cancelled = 3
}
