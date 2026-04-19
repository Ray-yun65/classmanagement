using System.ComponentModel.DataAnnotations;
using 프로젝트관리.Models;

namespace 프로젝트관리.ViewModels;

public class ClassFormViewModel
{
    public int ClassId { get; set; }

    [Display(Name = "과정명")]
    [Required(ErrorMessage = "과정명을 입력하세요.")]
    [StringLength(100)]
    public string ClassName { get; set; } = string.Empty;

    [Display(Name = "대상 부서")]
    [Required(ErrorMessage = "대상 부서를 입력하세요.")]
    [StringLength(100)]
    public string TargetDepartment { get; set; } = string.Empty;

    [Display(Name = "상태")]
    [Required]
    public ClassStatus Status { get; set; } = ClassStatus.Planned;

    [Display(Name = "설명")]
    [StringLength(1000)]
    public string? Description { get; set; }
}
