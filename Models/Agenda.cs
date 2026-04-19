using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace 프로젝트관리.Models;

public class Agenda : IValidatableObject
{
    [Key]
    public int AgendaId { get; set; }

    [Required]
    public int MeetingId { get; set; }

    [ForeignKey(nameof(MeetingId))]
    public Meeting? Meeting { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    public int? RelatedClassId { get; set; }
    public EducationClass? RelatedClass { get; set; }

    public int? RelatedSessionId { get; set; }
    public Session? RelatedSession { get; set; }

    [Required]
    [StringLength(100)]
    public string Requester { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Owner { get; set; } = string.Empty;

    [Required]
    public AgendaPriority Priority { get; set; } = AgendaPriority.Medium;

    [Required]
    public AgendaStatus Status { get; set; } = AgendaStatus.Open;

    [StringLength(2000)]
    public string? Decision { get; set; }

    [StringLength(2000)]
    public string? ActionItem { get; set; }

    public DateOnly? DueDate { get; set; }

    public bool IsReflected { get; set; }

    [NotMapped]
    public bool IsOverdue =>
        DueDate.HasValue &&
        DueDate.Value < DateOnly.FromDateTime(DateTime.Today) &&
        !IsReflected;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Status == AgendaStatus.Decided && string.IsNullOrWhiteSpace(Decision))
        {
            yield return new ValidationResult(
                "Status가 Decided일 때는 Decision이 필수입니다.",
                new[] { nameof(Decision), nameof(Status) });
        }

        if (IsReflected && RelatedClassId is null && RelatedSessionId is null)
        {
            yield return new ValidationResult(
                "IsReflected가 true이면 RelatedClassId 또는 RelatedSessionId가 필요합니다.",
                new[] { nameof(IsReflected), nameof(RelatedClassId), nameof(RelatedSessionId) });
        }
    }
}
