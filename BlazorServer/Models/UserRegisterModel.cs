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

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Phone is required")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "HealthCard is required")]
    public string HealthCard { get; set; }

    [Required(ErrorMessage = "HomeAddress is required")]
    public string HomeAddress { get; set; }
}