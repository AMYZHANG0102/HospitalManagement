using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>?> GetAllAsync();
    Task<Appointment?> GetByIdAsync(long id);
    Task<IEnumerable<Appointment>?> GetByDoctorIdAsync(long id);
    Task<IEnumerable<Appointment>?> GetByPatientIdAsync(long id);
    Task<IEnumerable<Appointment>?> GetByStatusAsync(Status status);
    Task<IEnumerable<Appointment>?> GetByTypeAsync(AppointmentType type);
    Task<IEnumerable<Appointment>?> GetByDateAsync(DateOnly date);
    Task<Appointment> CreateAsync(Appointment appointment);
    Task<Appointment?> UpdateAsync(Appointment appointment);
    Task<bool> DeleteAsync(long id);
    Task<bool> ExistsAsync(long id);
}