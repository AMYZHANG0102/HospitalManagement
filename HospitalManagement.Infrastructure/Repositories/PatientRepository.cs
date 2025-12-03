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
    public async Task<Patient?> GetByIdAsync(int id)
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
        return await _context.Patients.AnyAsync(p => p.Id == id);
    }
}