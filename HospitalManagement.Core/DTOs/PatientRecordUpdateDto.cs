using HospitalManagement.Core.Models;
namespace TaskManagement.Core.DTOs;

public class PatientRecordUpdateDto
{
    public List<RecordEntry>? Entries { get; set; }
}