using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IAdminService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<IEnumerable<User>> GetAllAdminsAsync();
    Task<IEnumerable<Patient>> GetAllPatientsAsync();
    Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
    Task<IEnumerable<Shift>> GetAllShiftsAsync();
    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();

    Task<User?> GetUserAsync(long id);
    Task<Patient?> GetPatientByIdAsync(long id);
    Task<Doctor?> GetDoctorByIdAsync(long id);
    Task<Shift?> GetShiftByIdAsync(long id);
    Task<Appointment?> GetAppointmentByIdAsync(long id);

    Task<User> CreateUserAsync(User user);
    Task<Patient> CreatePatientAsync(Patient patient);
    Task<Doctor> CreateDoctorAsync(Doctor doctor);
    Task<Shift> CreateShiftAsync(Shift shift);
    Task<Appointment> CreateAppointmentAsync(Appointment appointment);

    Task<User?> UpdateUserAsync(User user);
    Task<Patient?> UpdatePatientAsync(Patient patient);
    Task<Doctor?> UpdateDoctorAsync(Doctor doctor);
    Task<Shift?> UpdateShiftAsync(Shift shift);
    Task<Appointment?> UpdateAppointmentAsync(Appointment appointment);
}