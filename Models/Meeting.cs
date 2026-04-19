using System.ComponentModel.DataAnnotations;

namespace 프로젝트관리.Models;

public class Meeting
{
    [Key]
    public int MeetingId { get; set; }

    [Required]
    public DateOnly MeetingDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Required]
    [StringLength(200)]
    public string MeetingName { get; set; } = string.Empty;

    [Required]
    public MeetingType MeetingType { get; set; } = MeetingType.Regular;

    public ICollection<Agenda> Agendas { get; set; } = new List<Agenda>();
}
