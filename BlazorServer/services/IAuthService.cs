using BlazorServer.Models;
namespace BlazorServer.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterModel request);
    Task<string> LoginAsync(UserLoginModel request);
}