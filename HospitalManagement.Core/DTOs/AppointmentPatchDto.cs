/* Amy Zhang
Summary: AppointmentPatchDto is used for updating existing appointment records. */

using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.DTOs;

public class AppointmentPatchDto
{
    public string DoctorId { get; set; } // Foreign Key
    
    public AppointmentType Type { get; set; }

    public DateTime DateTime { get; set; }

    public AppointmentStatus Status { get; set; }

    public bool DoctorIsUnavaliable { get; set; } // When doctor marks unavailability
}