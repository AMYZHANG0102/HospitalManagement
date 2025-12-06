/*Hira Ahmad
Summary: IPatientRepository represents the interface for managing patient data.
*/
using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllPatientsAsync();
    Task<Patient?> GetPatientByIdAsync(string id);
    Task<Patient> CreatePatientAsync(Patient patient);
    Task<Patient?> UpdatePatientAsync(Patient patient);
    Task<bool> DeleteAsync(string id);
    Task<IEnumerable<Appointment>> GetAllPatientAppointmentsAsync(string id);
    Task<IEnumerable<Review>> GetAllPatientReviewsAsync(string id);
}
