using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
namespace HospitalManagement.Infrastrucutre.Services;

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepo;
    private readonly IPatientRepository _patientRepo;
    private readonly IDoctorRepository _doctorRepo;
    private readonly IShiftRepository _shiftRepo;
    private readonly IAppointmentRepository _appointmentRepo;

    public AdminService(
        IUserRepository userRepo,
        IPatientRepository patientRepo,
        IDoctorRepository doctorRepo,
        IShiftRepository shiftRepo,
        IAppointmentRepository appointmentRepo)
    {
        _userRepo = userRepo;
        _patientRepo = patientRepo;
        _doctorRepo = doctorRepo;
        _shiftRepo = shiftRepo;
        _appointmentRepo = appointmentRepo;
    }

    #region Get All Methods
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepo.GetAllAsync();
    }

    public async Task<IEnumerable<User>> GetAllAdminsAsync()
    {
        var users = await _userRepo.GetAllAsync();
        return users.Where(u => u.Role == UserRole.Admin);
    }

    public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        return await _patientRepo.GetAllAsync();
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
    {
        return await _doctorRepo.GetAllAsync();
    }

    public async Task<IEnumerable<Shift>> GetAllShiftsAsync()
    {
        return await _shiftRepo.GetAllAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
    {
        return await _appointmentRepo.GetAllAsync();
    }
    #endregion

    #region Get By Id Methods
    public async Task<User?> GetUserAsync(long id)
    {
        return await _userRepo.GetByIdAsync(id);
    }

    public async Task<Patient?> GetPatientByIdAsync(long id)
    {
        return await _patientRepo.GetByIdAsync(id);
    }

    public async Task<Doctor?> GetDoctorByIdAsync(long id)
    {
        return await _doctorRepo.GetByIdAsync(id);
    }

    public async Task<Shift?> GetShiftByIdAsync(long id)
    {
        return await _shiftRepo.GetByIdAsync(id);
    }

    public async Task<Appointment?> GetAppointmentByIdAsync(long id)
    {
        return await _appointmentRepo.GetByIdAsync(id);
    }
    #endregion

    #region Create Methods
    public async Task<User> CreateUserAsync(User user)
    {
        return await _userRepo.CreateAsync(user);
    }

    public async Task<Patient> CreatePatientAsync(Patient patient)
    {
        return await _patientRepo.CreateAsync(patient);
    }

    public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
    {
        return await _doctorRepo.CreateAsync(doctor);
    }

    public async Task<Shift> CreateShiftAsync(Shift shift)
    {
        return await _shiftRepo.CreateAsync(shift);
    }

    public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
    {
        return await _appointmentRepo.CreateAsync(appointment);
    }
    #endregion

    #region Update Methods
    public async Task<User?> UpdateUserAsync(User user)
    {
        return await _userRepo.UpdateAsync(user);
    }

    public async Task<Patient?> UpdatePatientAsync(Patient patient)
    {
        return await _patientRepo.UpdateAsync(patient);
    }

    public async Task<Doctor?> UpdateDoctorAsync(Doctor doctor)
    {
        return await _doctorRepo.UpdateAsync(doctor);
    }

    public async Task<Shift?> UpdateShiftAsync(Shift shift)
    {
        return await _shiftRepo.UpdateAsync(shift);
    }

    public async Task<Appointment?> UpdateAppointmentAsync(Appointment appointment)
    {
        return await _appointmentRepo.UpdateAsync(appointment);
    }
    #endregion
      
}