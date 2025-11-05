using System.ComponentModel.DataAnnotations;
namespace TaskManagement.Core.DTOs;

public class UserCreateDto
{
    [Required (ErrorMessage = "Name is required")]
    [StringLength (50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string Name { get; set; } = string.Empty;

    [Required (ErrorMessage = "Surname is required")]
    [StringLength (50, ErrorMessage = "Surname cannot exceed 50 characters")]
    public string Surname { get; set; } = string.Empty;

    [Required (ErrorMessage = "User role needs to be specified")]
    public Role Role { get; set; }

    [Required (ErrorMessage = "Phone is required")]
    [RegularExpression (@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone must be in format xxx-xxx-xxxx")]
    public string Phone { get; set; } = string.Empty;

    [Required (ErrorMessage = "Email is required")]
    [EmailAddress (ErrorMessage = "Invalid email format")]
    [StringLength (100, ErrorMessage = "Address cannot exceed 100 characters")]
    public string Email { get; set; } = string.Empty;

    [Required (ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;

    [Required (ErrorMessage = "Birthdate is required")]
    public DateOnly Birthdate { get; set; }

    [Required (ErrorMessage = "Address is required")]
    [StringLength (100, ErrorMessage = "Address cannot exceed 100 characters")]
    public string Address { get; set; } = string.Empty;
}