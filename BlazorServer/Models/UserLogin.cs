/* Hira Ahmad
Summary: UserLoginModel class to represent user login data. */

using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.BlazorServer.Models;

public class UserLoginModel 
{
    public string Email { get; set; }
    public string Password { get; set; }
}

