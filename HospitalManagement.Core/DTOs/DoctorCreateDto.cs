/* Amy Zhang
Summary: DoctorCreateDto extends UserCreateDto to include doctor-specific properties.
It adds a required Specialization field to capture the doctor's area of expertise. */

using System.ComponentModel.DataAnnotations;
using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.DTOs; 

public class DoctorCreateDto : UserCreateDto
{
    [Required (ErrorMessage = "Specialization must be stated")]
    public Specialization Specialization { get; set; }
}