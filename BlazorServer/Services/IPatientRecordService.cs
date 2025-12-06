using HospitalManagement.BlazorServer.Models;
namespace HospitalManagement.BlazorServer.Services;

public interface IPatientRecordService
{
    Task<List<PatientRecord>> GetAllPatientRecordsAsync();
}