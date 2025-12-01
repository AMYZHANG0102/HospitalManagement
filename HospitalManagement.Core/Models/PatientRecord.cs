/* Amy Zhang
Summary: This class represents a patient record/medical history containing multiple entries.*/

using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class PatientRecord
{
    public long Id { get; set; } // Primary Key

    [Required]
    public long PatientId { get; set; } // Foreign Key: To which patient does this record belong to?

    public List<RecordEntry> Entries { get; set; } = new(); // Initially empty
}