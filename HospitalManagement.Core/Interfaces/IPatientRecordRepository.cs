using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IPatientRecordRepository
{
    Task<IEnumerable<PatientRecord>> GetAllAsync();
    Task<PatientRecord?> GetByIdAsync(int id);
    Task<PatientRecord> CreateAsync(PatientRecord patientRecord);
    Task<PatientRecord?> UpdateAsync(PatientRecord patientRecord);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
