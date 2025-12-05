/* Amy Zhang
Summary: This DTO is used for creating new shifts. */

using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.DTOs;

public class ShiftCreateDto
{
    [Required (ErrorMessage = "Specify doctors working on this shift")]
    public string DoctorId { get; set; }

    [Required (ErrorMessage = "Shift starting time is required")]
    public DateTime StartDateTime { get; set; }
    
    [Required(ErrorMessage = "Shift ending time is required")]
    public DateTime EndDateTime { get; set; }
}