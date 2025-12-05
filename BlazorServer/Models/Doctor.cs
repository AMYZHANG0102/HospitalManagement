using System.ComponentModel.DataAnnotations;
namespace BlazorServer.Models;

public class Doctor
{

    [Required]
    public string DoctorId { get; set; } = string.Empty;
    
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string specialization { get; set; } = string.Empty;

    [Required]
    public string Gender { get; set; } = string.Empty;

    [Required]
    public bool IsAvailable { get; set; } = false;

    public string TimeSlot { get; set; } = string.Empty;

    public string Date { get; set; } = string.Empty;

    
}