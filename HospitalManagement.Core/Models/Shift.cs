/* Amy Zhang
Summary: This class represents a work shift for doctors. */

using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Shift
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Specify doctors working on this shift")]
    public List<Doctor> Doctors { get; set; } = new(); // Many-to-Many relationship
    
    [Required (ErrorMessage = "Shift date is required")]
    public DateOnly Date { get; set; }
    
    [Required (ErrorMessage = "Shift starting time is required")]
    public TimeOnly StartTime { get; set; }
    
    [Required(ErrorMessage = "Shift ending time is required")]
    public TimeOnly EndTime { get; set; }
}