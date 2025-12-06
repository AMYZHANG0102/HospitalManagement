using HospitalManagement.BlazorServer.Models;

namespace HospitalManagement.BlazorServer.Services;

public interface IDoctorService
{
    Task<List<Doctor>> GetAllDoctorsAsync();
}