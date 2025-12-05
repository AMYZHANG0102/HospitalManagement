/*Hira
Summary: Data Transfer Object for updating an existing patient record.*/
namespace HospitalManagement.Core.DTOs;
public class PatientRecordUpdateDto
{
    public string PatientId { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string CurrentMedications { get; set; } = string.Empty;
    public string MedicationHistory { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}