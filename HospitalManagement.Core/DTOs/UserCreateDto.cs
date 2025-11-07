using System.ComponentModel.DataAnnotations;
using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.DTOs;

public class UserCreateDto
{
    [Required (ErrorMessage = "First Name is required")]
    [StringLength (50, ErrorMessage = "First Name cannot exceed 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required (ErrorMessage = "Last Name is required")]
    [StringLength (50, ErrorMessage = "Last Name cannot exceed 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required (ErrorMessage = "User role needs to be specified")]
    public Role Role { get; set; }

    [Required (ErrorMessage = "Phone is required")]
    [RegularExpression (@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone must be in format xxx-xxx-xxxx")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
    public string Email { get; set; } = string.Empty;
    
    [Required (ErrorMessage = "Gender is required")]
    public Gender Gender { get; set; }

    [Required (ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;

    [Required (ErrorMessage = "Birthdate is required")]
    public DateOnly Birthdate { get; set; }

    [Required (ErrorMessage = "Home Address is required")]
    [StringLength (100, ErrorMessage = "Home Address cannot exceed 100 characters")]
    public string HomeAddress { get; set; } = string.Empty;
}