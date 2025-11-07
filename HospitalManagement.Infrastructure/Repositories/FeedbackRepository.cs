using Microsoft.EntityFrameworkCore;
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using HospitalManagement.Infrastructure.Data;

namespace HospitalManagement.Infrastructure.Repositories;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly ApplicationDbContext _context;
    public FeedbackRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Feedback>?> GetAllAsync()
    {
        return await _context.Feedbacks.ToListAsync();
    }

    public async Task<Feedback?> GetByIdAsync(long id)
    {
        return await _context.Feedbacks.FindAsync(id);
    }
    public async Task<IEnumerable<Feedback>?> GetByDoctorIdAsync(long doctorId)
    {
        return await _context.Feedbacks
            .Where(f => f.DoctorId == doctorId)
            .OrderByDescending(f => f.Date)
            .ToListAsync();
    }
    public async Task<IEnumerable<Feedback>?> GetByRatingAsync(Rating rating)
    {
        return await _context.Feedbacks
            .Where(f => f.Rating == rating)
            .OrderByDescending(f => f.Date)
            .ToListAsync();
    }
    public async Task<IEnumerable<Feedback>?> GetByDateAsync(DateOnly date)
    {
        return await _context.Feedbacks
            .Where(f => f.Date == date)
            .ToListAsync();
    }
    public async Task<double> GetAverageDoctorRatingAsync(long doctorId)
    {
        var feedbacks = await _context.Feedbacks
            .Where(f => f.DoctorId == doctorId)
            .ToListAsync();

        if (!feedbacks.Any())
        {
            return 0;
        }

        // Convert Rating enum to numeric value (1-5)
        var averageRating = feedbacks.Average(f => (int)f.Rating);
        return Math.Round(averageRating, 2);
    }
    public async Task<Feedback> CreateAsync(Feedback feedback)
    {
        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();
        return feedback;
    }
    public async Task<bool> DeleteAsync(long id)
    {
        var feedback = await _context.Feedbacks.FindAsync(id);
        if (feedback == null)
        {
            return false;
        }

        _context.Feedbacks.Remove(feedback);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(long id)
    {
        return await _context.Feedbacks.AnyAsync(f => f.Id == id);
    }

}