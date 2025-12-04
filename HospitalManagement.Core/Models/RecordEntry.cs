/* Amy Zhang
Summary: This class represents a record entry made by a doctor for a patient. */

using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class RecordEntry
{
    public int Id { get; set; } // Primary Key

    [Required]
    public int PatientRecordId { get; set; } // Foreign Key: To which patient record does this entry belong to?

    [Required]
    public string DoctorId { get; set; } // Doctor who created the entry

    [Required (ErrorMessage = "Provide a description for this entry")]
    public string Description { get; set; } = string.Empty;

    public DateTime DateTime { get; set; } = DateTime.Now;

    // Navigation properties
    public PatientRecord? PatientRecord { get; set; }
    public Doctor? Doctor { get; set; }
}