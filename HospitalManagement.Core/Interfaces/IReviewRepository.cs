using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync();
    Task<Review?> GetByIdAsync(long id);
    Task<IEnumerable<Review>> GetByDoctorIdAsync(long doctorId);
    Task<IEnumerable<Review>> GetByRatingAsync(Rating rating);
    Task<IEnumerable<Review>> GetByDateAsync(DateOnly date);
    Task<double> GetAverageDoctorRatingAsync(long doctorId);
    Task<Review> CreateAsync(Review Review);
    Task<bool> DeleteAsync(long id);
    Task<bool> ExistsAsync(long id);
}