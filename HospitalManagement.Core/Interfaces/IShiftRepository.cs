/* Amy Zhang
Summary: Interface for Shift Repository defining CRUD operations and queries*/

using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IShiftRepository
{
    Task<IEnumerable<Shift>> GetAllAsync();
    Task<Shift?> GetByIdAsync(int id);
    Task<IEnumerable<Shift>> GetByDoctorIdAsync(string id);
    Task<Shift> CreateAsync(Shift shift);
    Task<Shift?> UpdateAsync(Shift shift);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}