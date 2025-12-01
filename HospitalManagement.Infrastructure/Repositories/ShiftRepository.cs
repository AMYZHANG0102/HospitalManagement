using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using HospitalManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace HospitalManagement.Infrastructure.Repositories;


public class ShiftRepository : IShiftRepository
{
    private readonly ApplicationDbContext _context;
    public ShiftRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Shift>> GetAllAsync()
    {
        return await _context.Shifts.ToListAsync();
    }

    public async Task<Shift?> GetByIdAsync(long id)
    {
        return await _context.Shifts.FindAsync(id);
    }

    public async Task<IEnumerable<Shift>> GetByDoctorIdAsync(long id)
    {
        return await _context.Shifts
                             .Where(e => e.Id == id)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Shift>> GetByDateAsync(DateOnly date)
    {
        return await _context.Shifts
                             .Where(e => e.Date == date)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Shift>> GetByTypeAsync(ShiftType type)
    {
        return await _context.Shifts
                             .Where(e => e.Type == type)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Shift>> GetByStatusAsync(ShiftStatus status)
    {
        return await _context.Shifts
                             .Where(e => e.Status == status)
                             .ToListAsync();
    }

    public async Task<Shift> CreateAsync(Shift shift)
    {
        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();
        return shift;
    }

    public async Task<Shift?> UpdateAsync(Shift shift)
    {
        var existingShift = await _context.Shifts.FindAsync(shift.Id);
        if (existingShift == null)
        {
            return null;
        }
        existingShift.DoctorId = shift.DoctorId;
        existingShift.Date = shift.Date;
        existingShift.StartTime = shift.StartTime;
        existingShift.EndTime = shift.EndTime;
        existingShift.Type = shift.Type;
        existingShift.Status = shift.Status;
        await _context.SaveChangesAsync();
        return existingShift;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var shift = await _context.Shifts.FindAsync(id);
        if (shift == null)
        {
            return false;
        }
        _context.Shifts.Remove(shift);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> ExistsAsync(long id)
    {
        return await _context.Shifts.AnyAsync(e => e.Id == id);
    }
}