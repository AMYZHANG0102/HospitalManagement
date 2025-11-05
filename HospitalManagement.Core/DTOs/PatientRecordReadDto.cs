using HospitalManagement.Core.Models;
namespace TaskManagement.Core.DTOs;

public class PatientRecordReadDto
{
    public long Id { get; set; } // Primary Key

    public long PatientId { get; set; } // Foreign Key: To which patient does this record belong to?

    public List<RecordEntry> Entries { get; set; } = new(); // Initially empty
}