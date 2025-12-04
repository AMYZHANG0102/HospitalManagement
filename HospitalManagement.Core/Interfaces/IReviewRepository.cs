using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync();
    Task<Review?> GetByIdAsync(int id);
    Task<IEnumerable<Review>> GetByDoctorIdAsync(string doctorId);
    Task<IEnumerable<Review>> GetByRatingAsync(Rating rating);
    Task<IEnumerable<Review>> GetByDateAsync(DateOnly date);
    Task<double> GetAverageDoctorRatingAsync(string doctorId);
    Task<Review> CreateAsync(Review Review);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}