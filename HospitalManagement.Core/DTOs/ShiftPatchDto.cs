/* Amy Zhang
Summary: This DTO is used for patching existing shifts. */

namespace HospitalManagement.Core.DTOs;

public class ShiftPatchDto
{
    public string DoctorId { get; set; } // Foreign key list

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }
}