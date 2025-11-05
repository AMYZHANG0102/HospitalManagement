using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Patient
{
    [Required(ErrorMessage = "Health card is required")]
    [RegularExpression(@"^\d{4}-\d{3}-\d{3}$", ErrorMessage = "Healthcard must be in format xxxx-xxx-xxx")]
    public string HealthCard { get; set; } = string.Empty;

    public PatientRecord Record { get; set; } = new(); // Navigation property

    public List<Appointment> Appointments { get; set; } = new(); // Initially empty

    public List<Feedback> SentFeedback { get; set; } = new();
}