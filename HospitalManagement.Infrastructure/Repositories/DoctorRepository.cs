using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using HospitalManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace HospitalManagement.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationDbContext _context;
    public DoctorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        return await _context.Doctors.ToListAsync();
    }

    public async Task<Doctor?> GetByIdAsync(long id)
    {
        return await _context.Doctors.FindAsync(id);
    }

    public async Task<IEnumerable<Doctor>> GetBySpecializationAsync(Specialization specialization)
    {
        return await _context.Doctors
                             .Where(e => e.Specialization == specialization)
                             .ToListAsync();
    }

    public async Task<Doctor> CreateAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }

    public async Task<Doctor?> UpdateAsync(Doctor doctor)
    {
        var existingDoctor = await _context.Doctors.FindAsync(doctor.Id);
        if (existingDoctor == null)
        {
            return null;
        }
        existingDoctor.FirstName = doctor.FirstName;
        existingDoctor.LastName = doctor.LastName;
        existingDoctor.Phone = doctor.Phone;
        existingDoctor.Email = doctor.Email;
        existingDoctor.Gender = doctor.Gender;
        existingDoctor.Birthdate = doctor.Birthdate;
        existingDoctor.HomeAddress = doctor.HomeAddress;
        await _context.SaveChangesAsync();
        return existingDoctor;
    }
}