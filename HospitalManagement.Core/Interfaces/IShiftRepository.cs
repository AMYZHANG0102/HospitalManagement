/* Amy Zhang
Summary: Interface for Shift Repository defining CRUD operations and queries*/

using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IShiftRepository
{
    Task<IEnumerable<Shift>> GetAllAsync();
    Task<Shift?> GetByIdAsync(long id);
    Task<IEnumerable<Shift>> GetByDoctorIdAsync(long id);
    Task<IEnumerable<Shift>> GetByDateAsync(DateOnly date);
    Task<IEnumerable<Shift>> GetByTypeAsync(ShiftType type);
    Task<IEnumerable<Shift>> GetByStatusAsync(ShiftStatus status);
    Task<Shift> CreateAsync(Shift shift);
    Task<Shift?> UpdateAsync(Shift shift);
    Task<bool> DeleteAsync(long id);
    Task<bool> ExistsAsync(long id);
}