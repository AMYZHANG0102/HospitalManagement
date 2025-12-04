/* Hira Ahmad
Summary: PatientRecordRepository implements IPatientRecordRepository to manage patient records in the database.
It provides methods for CRUD operations on patient records.*/

using Microsoft.EntityFrameworkCore;
using HospitalManagement.Core.Models;
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Infrastructure.Data;
namespace HospitalManagement.Infrastructure.Repositories;

public class PatientRecordRepository : IPatientRecordRepository
{
    private readonly ApplicationDbContext _context;
    public PatientRecordRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<PatientRecord>> GetAllAsync()
    {
        return await _context.PatientRecords.ToListAsync();
    }
    public async Task<PatientRecord?> GetByIdAsync(int id)
    {
        return await _context.PatientRecords.FindAsync(id);
    }
    public async Task<PatientRecord> CreateAsync(PatientRecord patientRecord)
    {
        _context.PatientRecords.Add(patientRecord);
        await _context.SaveChangesAsync();
        return patientRecord;
    }
    public async Task<PatientRecord?> UpdateAsync(PatientRecord patientRecord)
    {
        var existingPatientRecord = await _context.PatientRecords.FindAsync(patientRecord.Id);
        if (existingPatientRecord == null)
        {
            return null;
        }
        existingPatientRecord.PatientId = patientRecord.PatientId;
        await _context.SaveChangesAsync();
        return existingPatientRecord;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var patientRecord = await _context.PatientRecords.FindAsync(id);
        if (patientRecord == null)
        {
            return false;
        }
        _context.PatientRecords.Remove(patientRecord);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.PatientRecords.AnyAsync(p => p.Id == id);
    }
}