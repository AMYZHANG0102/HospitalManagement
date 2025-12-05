/* Amy Zhang
Summary: This class represents reviews given by patients.
The review can be about a doctor (optional) or about the hospital service in general.
Patients can provide a comment on their review (optional),
but the rating is necessary. */

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace HospitalManagement.Core.Models;

public class Review
{
    public int Id { get; set; }

    [Required]
    public string PatientId { get; set; }

    public string? DoctorId { get; set; } // Optional

    [Required (ErrorMessage = "Please provide a rating")]
    public Rating Rating { get; set; }

    [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
    public string? Comment { get; set; } = string.Empty;
    
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    // Navigation properties
    [JsonIgnore]
    public Patient? Patient { get; set; }
    [JsonIgnore]
    public Doctor? Doctor { get; set; }
}