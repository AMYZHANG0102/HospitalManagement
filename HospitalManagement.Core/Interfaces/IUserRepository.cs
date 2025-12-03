/* Amy Zhang
Summay: Repository interface to handle things that belong to ALL users.
This interface represents a generic repository. */

using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<User>> GetByRoleAsync(UserRole role);
    Task<User?> GetByIdAsync(long id);
    Task<User?> GetByEmailAsync(string email);
    Task<User> CreateAsync(User user);
    Task<User?> UpdateAsync(User user);
    Task<bool> DeleteAsync(long id);
    Task<bool> ExistsAsync(long id);
    Task<bool> EmailExistsAsync(string email);
}