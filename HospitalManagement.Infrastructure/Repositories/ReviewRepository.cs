using Microsoft.EntityFrameworkCore;
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using HospitalManagement.Infrastructure.Data;

namespace HospitalManagement.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;
    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _context.Reviews.ToListAsync();
    }

    public async Task<Review?> GetByIdAsync(long id)
    {
        return await _context.Reviews.FindAsync(id);
    }
    public async Task<IEnumerable<Review>> GetByDoctorIdAsync(string doctorId)
    {
        return await _context.Reviews
            .Where(f => f.DoctorId == doctorId)
            .OrderByDescending(f => f.Date)
            .ToListAsync();
    }
    public async Task<IEnumerable<Review>> GetByRatingAsync(Rating rating)
    {
        return await _context.Reviews
            .Where(f => f.Rating == rating)
            .OrderByDescending(f => f.Date)
            .ToListAsync();
    }
    public async Task<IEnumerable<Review>> GetByDateAsync(DateOnly date)
    {
        return await _context.Reviews
            .Where(f => f.Date == date)
            .ToListAsync();
    }
    public async Task<double> GetAverageDoctorRatingAsync(string doctorId)
    {
        var Reviews = await _context.Reviews
            .Where(f => f.DoctorId == doctorId)
            .ToListAsync();

        if (!Reviews.Any())
        {
            return 0;
        }

        // Convert Rating enum to numeric value (1-5)
        var averageRating = Reviews.Average(f => (int)f.Rating);
        return Math.Round(averageRating, 2);
    }
    public async Task<Review> CreateAsync(Review Review)
    {
        _context.Reviews.Add(Review);
        await _context.SaveChangesAsync();
        return Review;
    }
    public async Task<bool> DeleteAsync(long id)
    {
        var Review = await _context.Reviews.FindAsync(id);
        if (Review == null)
        {
            return false;
        }

        _context.Reviews.Remove(Review);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(long id)
    {
        return await _context.Reviews.AnyAsync(f => f.Id == id);
    }

}