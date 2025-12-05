/* Amy Zhang
Summary: UserRepository implements the IUserRepository interface for managing
general user data in the database.
It provides methods for CRUD operations and querying users by role, email, or ID. */

using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using HospitalManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace HospitalManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<IEnumerable<User>> GetByRoleAsync(UserRole role)
    {
        return await _context.Users
                             .Where(u => u.Role == role)
                             .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await _context.Users
                             .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
                             .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);
        if (existingUser == null)
        {
            return null;
        }
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Phone = user.Phone;
        existingUser.Email = user.Email;
        existingUser.Gender = user.Gender;
        existingUser.Birthdate = user.Birthdate;
        existingUser.HomeAddress = user.HomeAddress;
        await _context.SaveChangesAsync();
        return existingUser;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null)
        {
            return false;
        }
        _context.Users.Remove(existingUser);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.Users.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}