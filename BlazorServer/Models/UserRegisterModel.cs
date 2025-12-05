/* Hira Ahmad
Summary: RegisterModel class to represent user registration data with validation. */
using System.ComponentModel.DataAnnotations;
namespace BlazorServer.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "UserName is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email Address is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string EmailAddress { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Phone Number is required")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Health Card is required")]
    public string HealthCard { get; set; }

    [Required(ErrorMessage = "Home Address is required")]
    public string HomeAddress { get; set; }
}