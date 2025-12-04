/* Hira Ahmad. */

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
    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        return await _context.Patients.ToListAsync();
    }
    public async Task<Patient?> GetByIdAsync(long id)
    {
        return await _context.Patients.FindAsync(id);
    }
    public async Task<Patient> CreateAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return patient;
    }
    public async Task<Patient?> UpdateAsync(Patient patient)
    {
        var existingPatient = await _context.Patients.FindAsync(patient.Id);
        if (existingPatient == null)
        {
            return null;
        }
        existingPatient.FirstName = patient.FirstName;
        existingPatient.LastName = patient.LastName;
        existingPatient.Birthdate = patient.Birthdate;
        existingPatient.Gender = patient.Gender;
        existingPatient.HealthCard = patient.HealthCard;
        existingPatient.Phone = patient.Phone;
        existingPatient.Email = patient.Email;
        existingPatient.Password = patient.Password;
        existingPatient.HomeAddress = patient.HomeAddress;
        existingPatient.Status = patient.Status;
        existingPatient.Role = patient.Role;
        await _context.SaveChangesAsync();
        return existingPatient;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
        {
            return false;
        }
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Patients.AnyAsync(p => p.Id == id);
    }
}