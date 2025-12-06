
using HospitalManagement.BlazorServer.Models;
using System.Net.Http.Headers;
namespace HospitalManagement.BlazorServer.Services;

public class PatientRecordService : IPatientRecordService
{
    private readonly HttpClient _http;
    private readonly IJWTService _jwtService;

    public PatientRecordService(HttpClient http, IJWTService jwtService)
    {
        _http = http;
        _jwtService = jwtService;
    }
    
    public async Task<List<PatientRecord>> GetAllPatientRecordsAsync()
    {
        if (!string.IsNullOrEmpty(_jwtService.Token))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _jwtService.Token);
        }

        var result = await _http.GetFromJsonAsync<List<PatientRecord>>("api/patientrecords");

        return result ?? new List<PatientRecord>();
    }
}