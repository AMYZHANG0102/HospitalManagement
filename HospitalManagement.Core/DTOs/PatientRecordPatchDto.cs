/*Iram
Summary: This DTO is used for patching existing patient records.*/
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Core.DTOs;

public class PatientRecordPatchDto
{
    [StringLength(500, ErrorMessage = "Diagnosis cannot exceed 500 characters")]
    public string? Diagnosis { get; set; }

    [StringLength(1000, ErrorMessage = "Medications cannot exceed 1000 characters")]
    public string? Medications { get; set; }

    [StringLength(2000, ErrorMessage = "Medication history cannot exceed 2000 characters")]
    public string? MedicationHistory { get; set; }

    [StringLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters")]
    public string? Notes { get; set; }
}