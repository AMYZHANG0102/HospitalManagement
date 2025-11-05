using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class RecordEntry
{
    [Required]
    public long DoctorId { get; set; } // Doctor who created the entry

    [Required]
    public DateTime DateTime { get; set; } = DateTime.Now;

    [Required (ErrorMessage = "Provide a description for this entry")]
    public string Description { get; set; } = string.Empty;
}