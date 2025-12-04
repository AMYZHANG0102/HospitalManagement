namespace HospitalManagement.Core.DTOs;

public class PatientRecordCreateDto
{
    public int PatientId { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string Medications { get; set; } = string.Empty;

}