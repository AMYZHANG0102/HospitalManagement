using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Appointment
{
    public long Id { get; set; }

    [Required (ErrorMessage = "Must specify patient")]
    public long PatientId { get; set; }

    [Required (ErrorMessage = "Must specify doctor")]
    public long DoctorId { get; set; }

    [Required (ErrorMessage = "Must specify appointment type")]
    public AppointmentType Type { get; set; }

    [Required (ErrorMessage = "Date and time are required")]
    public DateTime DateTime { get; set; }

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
}