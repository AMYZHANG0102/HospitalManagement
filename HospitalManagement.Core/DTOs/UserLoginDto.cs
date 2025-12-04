/*Hira
Summary: UserLoginDto represents the data transfer object for user login information.
*/
using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.DTOs;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
