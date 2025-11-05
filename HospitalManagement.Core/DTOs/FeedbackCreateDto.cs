using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class FeedbackCreateDto
{
    public long? DoctorId { get; set; } // Optional

    [Required (ErrorMessage = "Please provide a rating")]
    public Rating Rating { get; set; }

    [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
    public string? Comment { get; set; } = string.Empty;
}