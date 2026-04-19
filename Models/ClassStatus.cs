using System.ComponentModel.DataAnnotations;

namespace 프로젝트관리.Models;

public enum ClassStatus
{
    [Display(Name = "계획")]
    Planned = 1,
    [Display(Name = "진행")]
    Ongoing = 2,
    [Display(Name = "완료")]
    Completed = 3
}
