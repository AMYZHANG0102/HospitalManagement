/* Amy Zhang
Summary: Interface for Doctor Repository defining CRUD operations and queries*/

using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IDoctorRepository
{
    Task<IEnumerable<Doctor>> GetAllAsync();
    Task<Doctor?> GetByIdAsync(string id);
    Task<IEnumerable<Doctor>> GetBySpecializationAsync(Specialization specialization);
    Task<Doctor> CreateAsync(Doctor doctor);
    Task<Doctor?> UpdateAsync(Doctor doctor);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}