/* Amy Zhang
Summary: ReviewCreateDto defines the data needed for a patient to create a review.
It includes an optional DoctorId (the reivew can be doctor-specific or about the hospital in general),
a required Rating, and an optional Comment with length validation. */

using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class ReviewCreateDto
{
    public string DoctorId { get; set; } // Optional

    [Required (ErrorMessage = "Please provide a rating")]
    public Rating Rating { get; set; }

    [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
    public string? Comment { get; set; } = string.Empty;
}