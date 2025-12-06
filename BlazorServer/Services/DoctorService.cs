/*Hira Ahmad
Summary: DoctorService represents the service for managing doctor data.
*/
using HospitalManagement.BlazorServer.Models;
using System.Net.Http.Headers;

namespace HospitalManagement.BlazorServer.Services;

public class DoctorService : IDoctorService
{
    private readonly HttpClient _http;
    private readonly IJWTService _jwtService;

    public DoctorService(HttpClient http, IJWTService jwtService)
    {
        _http = http;
        _jwtService = jwtService;
    }
    
    public async Task<List<Doctor>> GetAllDoctorsAsync()
    {
        if (!string.IsNullOrEmpty(_jwtService.Token))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _jwtService.Token);
        }

        var result = await _http.GetFromJsonAsync<List<Doctor>>("api/doctors");

        return result ?? new List<Doctor>();
    }
}