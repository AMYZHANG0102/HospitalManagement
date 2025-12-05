/* Hira Ahmad
Summary: UserLoginModel class to represent user login data. */

using System.ComponentModel.DataAnnotations;
namespace BlazorServer.Models;

public class UserLoginModel 
{
    public string Username { get; set; }
    public string Password { get; set; }
}

