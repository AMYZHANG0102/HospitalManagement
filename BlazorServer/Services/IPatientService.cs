/*Hira Ahmad
Summary: IPatientService represents the interface for managing patient data.
*/
using HospitalManagement.BlazorServer.Models;
namespace HospitalManagement.BlazorServer.Services;

public interface IPatientService
{
    Task<string> EditPatientProfileAsync(Patient request);
}