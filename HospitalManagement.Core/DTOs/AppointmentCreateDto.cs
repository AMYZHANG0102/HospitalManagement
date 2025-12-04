/* Amy Zhang
Summary: AppointmentCreateDto is a Data Transfer Object (DTO) used for creating
new appointment records. */

using System.ComponentModel.DataAnnotations;
using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.DTOs;

public class AppointmentCreateDto
{
    [Required (ErrorMessage = "Must specify patient")]
    public long PatientId { get; set; } // Foreign Key

    [Required (ErrorMessage = "Must specify doctor")]
    public long DoctorId { get; set; } // Foreign Key

    [Required (ErrorMessage = "Must specify appointment type")]
    public AppointmentType Type { get; set; }

    [Required (ErrorMessage = "Date and time are required")]
    public DateTime DateTime { get; set; }
}