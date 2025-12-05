using System.ComponentModel.DataAnnotations;
namespace BlazorServer.Models;

public class PatientRecord
{
    public string Name { get; set; } = string.Empty;
    public string PatientId { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string Medications { get; set; } = string.Empty;

    public string MedicationAllergies { get; set; } = string.Empty;

    public string BloodType { get; set; } = string.Empty;

    public string PastMedicalHistory { get; set; } = string.Empty;

    public string CurrentMedications { get; set; } = string.Empty;

    public string LabTestsResults { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

}