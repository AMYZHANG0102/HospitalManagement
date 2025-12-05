/* Hira Ahmad
Summary: PatientRepository implements IPatientRepository to manage patients in the database. */

using Microsoft.EntityFrameworkCore;
using HospitalManagement.Core.Models;
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Infrastructure.Data;
namespace HospitalManagement.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;
    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        return await _context.Patients.ToListAsync();
    }
    public async Task<Patient?> GetPatientByIdAsync(string id)
    {
        return await _context.Patients.FindAsync(id);
    }
    public async Task<Patient> CreatePatientAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return patient;
    }
    public async Task<Patient?> UpdatePatientAsync(Patient patient)
    {
        var existingPatient = await _context.Patients.FindAsync(patient.Id);
        if (existingPatient == null)
        {
            return null;
        }
        existingPatient.Gender = patient.Gender;
        existingPatient.HealthCard = patient.HealthCard;
        existingPatient.Phone = patient.Phone;
        existingPatient.Email = patient.Email;
        existingPatient.Password = patient.Password;
        existingPatient.HomeAddress = patient.HomeAddress;
        existingPatient.Status = patient.Status;
        await _context.SaveChangesAsync();
        return existingPatient;
    }

    public async Task<Patient?> DeactivatePatientAsync(string id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
        {
            return null;
        }
        patient.IsDeactivated = true;
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<IEnumerable<Appointment>> GetAllPatientAppointmentsAsync(string id)
    {
        return await _context.Appointments
            .Where(a => a.PatientId == id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetAllPatientReviewsAsync(string id)
    {
        return await _context.Reviews
            .Where(r => r.PatientId == id)
            .ToListAsync();
    }
}