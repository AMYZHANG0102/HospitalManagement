/*Hira Ahmad
Summary: JWTService represents the service for managing JWT tokens.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace HospitalManagement.BlazorServer.Services;

public class JWTService : IJWTService
{
    public string? Token { get; private set; }

    public void SetToken(string token)
    {
        Token = token;
    }

    public void ClearToken()
    {
        Token = null;
    }
}