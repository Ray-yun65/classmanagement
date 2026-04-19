using System.ComponentModel.DataAnnotations;
using 프로젝트관리.ViewModels;

namespace 프로젝트관리.Validation;

public class SessionTimeRangeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is not SessionFormViewModel session)
        {
            return ValidationResult.Success;
        }

        if (session.EndTime <= session.StartTime)
        {
            return new ValidationResult("종료 시간은 시작 시간보다 늦어야 합니다.");
        }

        return ValidationResult.Success;
    }
}
