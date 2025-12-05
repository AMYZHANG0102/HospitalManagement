/* Amy Zhang
Summary: This class represents a work shift for doctors. */

using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Shift
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Specify doctors working on this shift")]
    public string DoctorId { get; set; } // Foreign Key

    public Doctor Doctor { get; set; } // Navigation property
    
    [Required (ErrorMessage = "Shift starting time is required")]
    public DateTime StartDateTime { get; set; }
    
    [Required(ErrorMessage = "Shift ending time is required")]
    public DateTime EndDateTime { get; set; }
}