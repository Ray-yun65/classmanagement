using System.ComponentModel.DataAnnotations;

namespace 프로젝트관리.Models;

public enum AgendaStatus
{
    [Display(Name = "열림")]
    Open = 1,
    [Display(Name = "결정")]
    Decided = 2,
    [Display(Name = "보류")]
    OnHold = 3
}
