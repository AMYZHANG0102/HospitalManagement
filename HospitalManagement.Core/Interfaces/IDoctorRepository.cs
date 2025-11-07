using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IDoctorRepository
{
    Task<IEnumerable<Doctor>?> GetAllAsync();
    Task<Doctor?> GetByIdAsync(long id);
    Task<IEnumerable<Doctor>?> GetBySpecializationAsync(Specialization specialization);
    Task<Doctor> CreateAsync(Doctor doctor);
    Task<Doctor?> UpdateAsync(Doctor doctor);
    Task<bool> DeleteAsync(long id);
    Task<bool> ExistsAsync(long id);
}