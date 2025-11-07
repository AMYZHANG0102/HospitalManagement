using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using HospitalManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace HospitalManagement.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _context;
    public AppointmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Appointment>?> GetAllAsync()
    {
        return await _context.Appointments.ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(long id)
    {
        return await _context.Appointments.FindAsync(id);
    }

    public async Task<IEnumerable<Appointment>?> GetByDoctorIdAsync(long id)
    {
        return await _context.Appointments
                             .Where(e => e.DoctorId == id)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>?> GetByPatientIdAsync(long id)
    {
        return await _context.Appointments
                             .Where(e => e.PatientId == id)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>?> GetByStatusAsync(Status status)
    {
        return await _context.Appointments
                             .Where(e => e.Status == status)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>?> GetByTypeAsync(AppointmentType type)
    {
        return await _context.Appointments
                             .Where(e => e.Type == type)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>?> GetByDateAsync(DateOnly date)
    {
        return await _context.Appointments
                             .Where(e => DateOnly.FromDateTime(e.DateTime) == date)
                             .ToListAsync();
    }

    public async Task<Appointment> CreateAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment?> UpdateAsync(Appointment appointment)
    {
        var existingAppointment = await _context.Appointments.FindAsync(appointment.Id);
        if (existingAppointment == null)
        {
            return null;
        }
        existingAppointment.DoctorId = appointment.DoctorId;
        existingAppointment.Type = appointment.Type;
        existingAppointment.DateTime = appointment.DateTime;
        existingAppointment.Status = appointment.Status;
        await _context.SaveChangesAsync();
        return existingAppointment;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return false;
        }
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> ExistsAsync(long id)
    {
        return await _context.Appointments.AnyAsync(e => e.Id == id);
    }
}