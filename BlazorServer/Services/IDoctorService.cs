/*Hira Ahmad
Summary: IDoctorService represents the interface for managing doctor data.
*/
using HospitalManagement.BlazorServer.Models;

namespace HospitalManagement.BlazorServer.Services;

public interface IDoctorService
{
    Task<List<Doctor>> GetAllDoctorsAsync();
}