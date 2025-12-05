using System.ComponentModel.DataAnnotations;
namespace BlazorServer.Models;

public class EditPatientProfile
{
    public string EmailAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string HealthCard { get; set; } = string.Empty;
    public string HomeAddress { get; set; } = string.Empty;
}