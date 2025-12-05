/* Amy Zhang
Summary: This class represents a patient record/medical history containing multiple entries.*/

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace HospitalManagement.Core.Models;

public class PatientRecord
{
    public int Id { get; set; } // Primary Key

    [Required (ErrorMessage = "PatientId is required")]
    public string PatientId { get; set; } // Foreign Key: To which patient does this record belong to?
    
    [JsonIgnore]
    public Patient? Patient { get; set; } // Navigation property

    public string MedicationAllergyInfo { get; set; } = string.Empty;

    public string BloodType { get; set; } = string.Empty;

    public string PastMedicalHistory { get; set; } = string.Empty;

    public string CurrentMedications { get; set; } = string.Empty;

    public string LabTestsResults { get; set; } = string.Empty;

    public string Diagnosis { get; set; } = string.Empty;

    [JsonIgnore]
    public List<RecordEntry> Entries { get; set; } = new(); // Initially empty
}