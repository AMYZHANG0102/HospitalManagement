/* Hira Ahmad
Summary: RegisterModel class to represent user registration data with validation. */
using System.ComponentModel.DataAnnotations;
namespace BlazorServer.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email Address is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone Number is required")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Health Card is required")]
    public string HealthCard { get; set; } = string.Empty;

    [Required(ErrorMessage = "Home Address is required")]
    public string HomeAddress { get; set; } = string.Empty;
}