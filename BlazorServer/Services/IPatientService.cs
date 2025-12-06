using HospitalManagement.BlazorServer.Models;
namespace HospitalManagement.BlazorServer.Services;

public interface IPatientService
{
    Task<string> EditPatientProfileAsync(Patient request);
}