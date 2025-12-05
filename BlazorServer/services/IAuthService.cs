using BlazorServer.Models;
namespace BlazorServer.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterModel request);
}