/* Amy Zhang
Summary: This child class represents a patient entity.
It inherits from the User class and has additional patient-related fields. */

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Patient : User
{
    [Required (ErrorMessage = "Health card is required")]
    [RegularExpression(@"^\d{4}-\d{3}-\d{3}$", ErrorMessage = "Healthcard must be in format xxxx-xxx-xxx")]
    [MaxLength(12)]
    public string HealthCard { get; set; } = string.Empty;

    public PatientRecord? PatientRecord { get; set; } // Navigation property

    [JsonIgnore]
    public List<Appointment> Appointments { get; set; } = new(); // Initially empty

    [JsonIgnore]
    public List<Review> ReviewsSent { get; set; } = new();
}