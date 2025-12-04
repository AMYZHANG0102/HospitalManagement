/* Amy Zhang
Summary: Interface for Appointment Repository defining CRUD operations and queries*/

using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAsync(
        long? patientId,
        long? doctorId,
        AppointmentStatus? status,
        DateOnly? date,
        AppointmentType? type);
    Task<Appointment?> GetByIdAsync(long id);
    Task<Appointment> CreateAsync(Appointment appointment);
    Task<Appointment?> UpdateAsync(Appointment appointment);
    Task<bool> DeleteAsync(long id);
    Task<bool> ExistsAsync(long id);
}