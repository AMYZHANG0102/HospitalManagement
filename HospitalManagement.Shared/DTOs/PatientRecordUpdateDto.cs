namespace HospitalManagement.Core.DTOs;

public class PatientRecordUpdateDto
{
    public string PatientId { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string Medications { get; set; } = string.Empty;
    public string MedicationHistory { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}