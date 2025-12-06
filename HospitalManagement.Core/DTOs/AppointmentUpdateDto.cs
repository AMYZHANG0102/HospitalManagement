/* Amy Zhang
Summary: AppointmentUpdateDto is a Data Transfer Object (DTO) used for updating
new appointment records. */

using System.ComponentModel.DataAnnotations;
using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.DTOs;

public class AppointmentUpdateDto
{
    [Required (ErrorMessage = "Must specify doctor")]
    public string DoctorId { get; set; } // Foreign Key

    [Required (ErrorMessage = "Must specify appointment type")]
    public AppointmentType Type { get; set; }

    [Required (ErrorMessage = "Date and time are required")]
    public DateTime DateTime { get; set; }

    [Required]
    public AppointmentStatus Status { get; set; }

    [Required]
    public bool DoctorIsUnavaliable { get; set; }
}