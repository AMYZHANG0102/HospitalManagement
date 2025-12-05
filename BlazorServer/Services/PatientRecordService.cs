
using BlazorServer.Models;
namespace BlazorServer.Services;

public class PatientRecordService : IPatientRecordService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public PatientRecordService(HttpClient http, IConfiguration configuration)
    {
        _http = http;
        _baseUrl = configuration["HospitalManagementApi:BaseUrl"];
    }
    
    public async Task<List<PatientRecord>> GetAllPatientRecordsAsync()
    {
        var url = $"{_baseUrl}/api/patients";
        var result = await _http.GetFromJsonAsync<List<PatientRecord>>(url);

        return result ?? new List<PatientRecord>();
    }
}