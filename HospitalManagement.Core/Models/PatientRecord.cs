using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class PatientRecord
{
    public long Id { get; set; }

    [Required]
    public long PatientId { get; set; } // To which patient does this record belong to?

    public List<RecordEntry>? Entries { get; set; }
}