/* Amy Zhang
Summary: Interface for Appointment Repository defining CRUD operations and queries*/

using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAsync(
        string? patientId,
        string? doctorId,
        AppointmentStatus? status,
        DateOnly? date,
        AppointmentType? type);

    Task<IEnumerable<Appointment>> GetAllWhereDoctorIsUnavailable();
        
    Task<Appointment?> GetByIdAsync(int id);
    Task<Appointment> CreateAsync(Appointment appointment);
    Task<Appointment?> UpdateAsync(Appointment appointment);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}