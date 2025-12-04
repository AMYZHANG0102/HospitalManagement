/*Hira
Summary: UserLoginDto represents the data transfer object for user login information.*/
using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required (ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}
