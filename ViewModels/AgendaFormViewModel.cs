using System.ComponentModel.DataAnnotations;
using 프로젝트관리.Models;

namespace 프로젝트관리.ViewModels;

public class AgendaFormViewModel : IValidatableObject
{
    public int AgendaId { get; set; }

    [Display(Name = "회의")]
    [Required]
    public int MeetingId { get; set; }

    [Display(Name = "제목")]
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "설명")]
    [StringLength(2000)]
    public string? Description { get; set; }

    [Display(Name = "연관 교육과정")]
    public int? RelatedClassId { get; set; }
    [Display(Name = "연관 세션")]
    public int? RelatedSessionId { get; set; }

    [Display(Name = "요청자")]
    [Required]
    [StringLength(100)]
    public string Requester { get; set; } = string.Empty;

    [Display(Name = "담당자")]
    [Required]
    [StringLength(100)]
    public string Owner { get; set; } = string.Empty;

    [Display(Name = "우선순위")]
    [Required]
    public AgendaPriority Priority { get; set; } = AgendaPriority.Medium;

    [Display(Name = "상태")]
    [Required]
    public AgendaStatus Status { get; set; } = AgendaStatus.Open;

    [Display(Name = "결정 사항")]
    [StringLength(2000)]
    public string? Decision { get; set; }

    [Display(Name = "조치 사항")]
    [StringLength(2000)]
    public string? ActionItem { get; set; }

    [Display(Name = "기한")]
    public DateOnly? DueDate { get; set; }
    [Display(Name = "반영 완료")]
    public bool IsReflected { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Status == AgendaStatus.Decided && string.IsNullOrWhiteSpace(Decision))
        {
            yield return new ValidationResult("결정 상태에서는 결정 사항을 입력해야 합니다.", new[] { nameof(Decision) });
        }

        if (IsReflected && !RelatedClassId.HasValue && !RelatedSessionId.HasValue)
        {
            yield return new ValidationResult("반영 완료 상태에서는 연관 과정 또는 세션을 선택해야 합니다.", new[] { nameof(RelatedClassId), nameof(RelatedSessionId) });
        }
    }
}
