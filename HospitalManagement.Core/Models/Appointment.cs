/* Amy Zhang
Summary: This class represents an appointment between a patient and a doctor. It has two foreign keys. */

using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Appointment
{
    public int Id { get; set; }

    [Required (ErrorMessage = "Must specify patient")]
    public int PatientId { get; set; } // Foreign Key

    [Required (ErrorMessage = "Must specify doctor")]
    public int DoctorId { get; set; } // Foreign Key

    [Required (ErrorMessage = "Must specify appointment type")]
    public AppointmentType Type { get; set; }

    [Required (ErrorMessage = "Date and time are required")]
    public DateTime DateTime { get; set; }

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

    // Navigation properties
    public Doctor? Doctor { get; set; }
    public Patient? Patient { get; set; }
}