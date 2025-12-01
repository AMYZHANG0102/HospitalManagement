/* Amy Zhang
Summary: This child class represents a patient entity.
It inherits from the User class and has additional patient-related fields. */

using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Patient : User
{
    [Required(ErrorMessage = "Health card is required")]
    [RegularExpression(@"^\d{4}-\d{3}-\d{3}$", ErrorMessage = "Healthcard must be in format xxxx-xxx-xxx")]
    public string HealthCard { get; set; } = string.Empty;

    public long RecordId { get; set; } // Foreign key
    public PatientRecord Record { get; set; } = new(); // Navigation property

    public List<Appointment> Appointments { get; set; } = new(); // Initially empty

    public List<Review> SentFeedback { get; set; } = new();
}