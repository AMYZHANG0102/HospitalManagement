using BlazorServer.Models;

namespace BlazorServer.Services;

public interface IDoctorService
{
    Task<List<Doctor>> GetAllDoctorsAsync();
}