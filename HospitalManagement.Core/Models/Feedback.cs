using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Feedback
{
    public long Id { get; set; }

    [Required]
    public long PatientId { get; set; }

    public long? DoctorId { get; set; } // Optional

    [Required (ErrorMessage = "Please provide a rating")]
    public Rating Rating { get; set; }

    [StringLength (500, ErrorMessage = "Comment cannot exceed 500 characters")]
    public string? Comment { get; set; }

    [Required]
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}