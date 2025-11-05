using System.ComponentModel.DataAnnotations;
using HospitalManagement.Core.Models;
namespace TaskManagement.Core.DTOs;

public class DoctorUpdateDto : UserUpdateDto
{
    [Required (ErrorMessage = "Specialization must be stated")]
    public Specialization Specialization { get; set; }
}