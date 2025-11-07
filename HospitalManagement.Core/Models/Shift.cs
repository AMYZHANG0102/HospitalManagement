using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Shift
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Specify doctors working on this shift")]
    public long DoctorId { get; set; } // Foreign key

    public Doctor? Doctor { get; set; } // Navigation Property
    
    [Required (ErrorMessage = "Shift date is required")]
    public DateOnly Date { get; set; }
    
    [Required (ErrorMessage = "Shift starting time is required")]
    public TimeOnly StartTime { get; set; }
    
    [Required(ErrorMessage = "Shift ending time is required")]
    public TimeOnly EndTime { get; set; }
    
    public ShiftType Type { get; set; }
    
    public Status Status { get; set; } = Status.Pending;
}