/*Hira Ahmad
Summary: IAuthService represents the interface for handling authentication operations.
*/
using HospitalManagement.BlazorServer.Models;
namespace HospitalManagement.BlazorServer.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterModel request);
    Task<string> LoginAsync(UserLoginModel request);
}