using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.DTOs;

public class PatientRecordUpdateDto
{
    public List<RecordEntry>? Entries { get; set; }
}