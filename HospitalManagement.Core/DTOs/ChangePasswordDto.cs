/*Hira Ahmad
Summary: ChangePasswordDto represents the data transfer object for changing a user's password.*/
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Core.DTOs
{
    public class ChangePasswordDto
    {    
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required] 
        [MinLength(6)] 
        public string NewPassword { get; set; } = string.Empty; 
        
        [Required] 
        [Compare(nameof(NewPassword))] 
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}