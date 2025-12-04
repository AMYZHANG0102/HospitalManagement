/*Hira
Summary: Data Transfer Object for creating a new patient record.*/

namespace HospitalManagement.Core.DTOs;

public class PatientRecordCreateDto
{
    public int PatientId { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string Medications { get; set; } = string.Empty;

    public string MedicationAllergyInfo { get; set; } = string.Empty;

    public string BloodType { get; set; } = string.Empty;

    public string PastMedicalHistory { get; set; } = string.Empty;

    public string CurrentMedications { get; set; } = string.Empty;

    public string LabTestsResults { get; set; } = string.Empty;

}