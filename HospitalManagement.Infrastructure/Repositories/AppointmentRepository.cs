/* Amy Zhang
Summary: AppointmentRepository implements the IAppointmentRepository interface
for managing appointment entities in the database using Entity Framework Core.
It performs CRUD operations on appointment records. */

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

    public async Task<IEnumerable<Appointment>> GetAllAsync(
        string? patientId,
        string? doctorId,
        AppointmentStatus? status,
        DateOnly? date,
        AppointmentType? type) 
    {
        var appointments = _context.Appointments.AsQueryable();
        if (patientId != null)
        {
            appointments = appointments.Where(e => e.PatientId == patientId);
        }
        if (doctorId != null)
        {
            appointments = appointments.Where(e => e.DoctorId == doctorId);
        }
        if (status != null)
        {
            appointments = appointments.Where(e => e.Status == status);
        }
        if (date != null)
        {
            appointments = appointments.Where(e => DateOnly.FromDateTime(e.DateTime) == date);
        }
        if (type != null)
        {
            appointments = appointments.Where(e => e.Type == type);
        }
        return await appointments.ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(int id) // For admins, doctors, and patients
    {
        return await _context.Appointments.FindAsync(id);
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

    public async Task<bool> DeleteAsync(int id)
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
    
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Appointments.AnyAsync(e => e.Id == id);
    }
}