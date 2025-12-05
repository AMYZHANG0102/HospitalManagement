using BlazorServer.Models;
namespace BlazorServer.Services;

public interface IPatientRecordService
{
    Task<List<PatientRecord>> GetAllPatientRecordsAsync();
}