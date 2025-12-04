/* Amy Zhang
Summary: Interface for Appointment Repository defining CRUD operations and queries*/

using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task<Appointment?> GetByIdAsync(long id);
    Task<IEnumerable<Appointment>> GetByDoctorIdAsync(string id);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(string id);
    Task<IEnumerable<Appointment>> GetByStatusAsync(AppointmentStatus status);
    Task<IEnumerable<Appointment>> GetByTypeAsync(AppointmentType type);
    Task<IEnumerable<Appointment>> GetByDateAsync(DateOnly date);
    Task<Appointment> CreateAsync(Appointment appointment);
    Task<Appointment?> UpdateAsync(Appointment appointment);
    Task<bool> DeleteAsync(long id);
    Task<bool> ExistsAsync(long id);
}