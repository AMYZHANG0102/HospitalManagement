/* Amy Zhang
Summary: UserPatchDto defines the properties that can be updated for a user via PATCH. */

using System.ComponentModel.DataAnnotations;
using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.DTOs;

public class UserPatchDto
{
    [StringLength (50, ErrorMessage = "First Name cannot exceed 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [StringLength (50, ErrorMessage = "Last Name cannot exceed 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [RegularExpression (@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone must be in format xxx-xxx-xxxx")]
    public string Phone { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public DateOnly Birthdate { get; set; }

    [StringLength (100, ErrorMessage = "Home Address cannot exceed 100 characters")]
    public string HomeAddress { get; set; } = string.Empty;

    public UserStatus? Status { get; set; }
    
}