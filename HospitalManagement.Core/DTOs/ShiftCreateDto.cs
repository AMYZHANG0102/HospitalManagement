/* Amy Zhang
Summary: This DTO is used for creating new shifts. */

using System.ComponentModel.DataAnnotations;
using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.DTOs;

public class ShiftCreateDto
{
    [Required (ErrorMessage = "Specify doctors working on this shift")]
    public List<Doctor> Doctors { get; set; } = new(); // Many-to-Many relationship
    
    [Required (ErrorMessage = "Shift date is required")]
    public DateOnly Date { get; set; }
    
    [Required (ErrorMessage = "Shift starting time is required")]
    public TimeOnly StartTime { get; set; }
    
    [Required (ErrorMessage = "Shift ending time is required")]
    public TimeOnly EndTime { get; set; }
}