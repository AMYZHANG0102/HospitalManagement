using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Patient
{
    [Required (ErrorMessage = "Health card is required")]
    [RegularExpression (@"^\d{4}-\d{3}-\d{3}$", ErrorMessage = "Healthcard must be in format xxxx-xxx-xxx")]
    public int HealthCard { get; set; }

    public PatientRecord? Record { get; set; }

    public List<Appointment>? Appointments { get; set; }

    public List<Feedback>? SentFeedback{ get; set; }
}