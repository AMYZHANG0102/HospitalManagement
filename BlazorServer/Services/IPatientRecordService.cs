/*Hira Ahmad
Summary: IPatientRecordService represents the interface for managing patient record data.
*/
using HospitalManagement.BlazorServer.Models;
namespace HospitalManagement.BlazorServer.Services;

public interface IPatientRecordService
{
    Task<List<PatientRecord>> GetAllPatientRecordsAsync();
}