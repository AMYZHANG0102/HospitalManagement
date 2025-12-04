/* Amy Zhang
Summary: DoctorUpdateDto extends UserUpdateDto to include doctor-specific properties.
It adds a required Specialization field to capture the doctor's area of expertise. */

using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.DTOs;

public class DoctorPatchDto : UserPatchDto
{
    public Specialization Specialization { get; set; }
}