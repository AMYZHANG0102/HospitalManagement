/* Amy Zhang
Summary: This DTO is used for patching existing shifts. */

using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.DTOs;

public class ShiftPatchDto
{
    public List<Doctor> Doctors { get; set; } = new(); // Many-to-Many relationship
    
    public DateOnly Date { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
}