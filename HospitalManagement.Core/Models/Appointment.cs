/* Amy Zhang
Summary: This class represents an appointment between a patient and a doctor. It has two foreign keys. */

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace HospitalManagement.Core.Models;

public class Appointment
{
    public int Id { get; set; }

    [Required (ErrorMessage = "Must specify patient")]
    public string PatientId { get; set; } // Foreign Key

    [Required (ErrorMessage = "Must specify doctor")]
    public string DoctorId { get; set; } // Foreign Key
    
    [Required (ErrorMessage = "Must specify appointment type")]
    public AppointmentType Type { get; set; }

    [Required (ErrorMessage = "Date and time are required")]
    public DateTime DateTime { get; set; }

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

    // Doctor is initially available, since the system only allows users to schedule appointments with available doctors.
    // But doctors may mark themselves as unavalable for an appointment later on, which changes this property to true.
    // The admin resolves doctor unavailability by re-assigning another docotr for the patient in the admin interface.
    public bool DoctorIsUnavailable { get; set; } = false;

    // Navigation properties
    [JsonIgnore]
    public Doctor? Doctor { get; set; }
    [JsonIgnore]
    public Patient? Patient { get; set; }
}