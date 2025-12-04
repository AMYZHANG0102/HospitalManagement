/* Amy Zhang
Summary: This class represents a patient record/medical history containing multiple entries.*/

using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class PatientRecord
{
    public int Id { get; set; } // Primary Key

    [Required (ErrorMessage = "PatientId is required")]
    public int PatientId { get; set; } // Foreign Key: To which patient does this record belong to?
    public Patient? Patient { get; set; } // Navigation property

    public List<RecordEntry> Entries { get; set; } = new(); // Initially empty
}