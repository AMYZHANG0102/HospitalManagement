using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(long id);
    Task<Patient> CreateAsync(Patient patient);
    Task<Patient?> UpdateAsync(Patient patient);
}
