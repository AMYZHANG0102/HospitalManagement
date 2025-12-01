/* Amy Zhang
Summary: This class represents reviews given by patients.
The review can be about a doctor (optional) or about the hospital service in general.
Patients can provide a comment on their review (optional),
but the rating is necessary. */

using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Review
{
    public long Id { get; set; }

    [Required]
    public long PatientId { get; set; }

    public long? DoctorId { get; set; } // Optional

    [Required (ErrorMessage = "Please provide a rating")]
    public Rating Rating { get; set; }

    [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
    public string? Comment { get; set; } = string.Empty;
    
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}