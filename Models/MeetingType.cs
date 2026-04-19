using System.ComponentModel.DataAnnotations;

namespace 프로젝트관리.Models;

public enum MeetingType
{
    [Display(Name = "정기")]
    Regular = 1,
    [Display(Name = "수시")]
    Adhoc = 2,
    [Display(Name = "긴급")]
    Urgent = 3
}
