/*Hira
Summary: RegisterDto represents the data transfer object for user registration information.
*/
using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.DTOs;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string? FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; } = string.Empty;

    [Required]
    public string UserName { get; set; } = string.Empty;
}

