using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.DTOs;

public class PatientCreateDto : UserCreateDto
{
    [Required(ErrorMessage = "Health card is required")]
    [RegularExpression(@"^\d{4}-\d{3}-\d{3}$", ErrorMessage = "Healthcard must be in format xxxx-xxx-xxx")]
    public string HealthCard { get; set; } = string.Empty;
}