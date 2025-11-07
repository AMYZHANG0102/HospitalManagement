using HospitalManagement.Core.Models;
namespace HospitalManagement.Core.Interfaces;

public interface IFeedbackRepository
{
    Task<IEnumerable<Feedback>?> GetAllAsync();
    Task<Feedback?> GetByIdAsync(long id);
    Task<IEnumerable<Feedback>?> GetByDoctorIdAsync(long doctorId);
    Task<IEnumerable<Feedback>?> GetByRatingAsync(Rating rating);
    Task<IEnumerable<Feedback>?> GetByDateAsync(DateOnly date);
    Task<double> GetAverageDoctorRatingAsync(long doctorId);
    Task<Feedback> CreateAsync(Feedback feedback);
    Task<bool> DeleteAsync(long id);
    Task<bool> ExistsAsync(long id);
}