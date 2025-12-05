using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BlazorServer.Services;

public interface IJWTService
{
    string? Token { get; }
    void SetToken(string token);
    void ClearToken();
}