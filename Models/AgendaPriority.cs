using System.ComponentModel.DataAnnotations;

namespace 프로젝트관리.Models;

public enum AgendaPriority
{
    [Display(Name = "높음")]
    High = 1,
    [Display(Name = "보통")]
    Medium = 2,
    [Display(Name = "낮음")]
    Low = 3
}
