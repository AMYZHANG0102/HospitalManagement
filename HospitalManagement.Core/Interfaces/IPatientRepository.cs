using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllPatientsAsync();
    Task<Patient?> GetPatientByIdAsync(string id);
    Task<Patient> CreatePatientAsync(Patient patient);
    Task<Patient?> UpdatePatientAsync(Patient patient);
    Task<Patient?> DeactivatePatientAsync(string id);
    Task<IEnumerable<Review>> GetAllPatientReviewsAsync(int id);
    Task<IEnumerable<Appointment>> GetAllPatientAppointmentsAsync(int id);
}
