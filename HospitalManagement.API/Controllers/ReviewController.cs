/*Iram
Summary: ReviewController represents the API controller for managing reviews from the patient.
Reviews - about service with a specific doctor or experience at the hospital.*/

using Microsoft.AspNetCore.Mvc;
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;

namespace HospitalManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IReviewRepository _repository;
    private readonly ILogger<ReviewsController> _logger;

    public ReviewsController( //reference repository
        IReviewRepository repository,
        ILogger<ReviewsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    // GET: api/reviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
    {
        var reviews = await _repository.GetAllAsync();
        return Ok(reviews);
    }

    // GET: api/reviews/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Review>> GetReview(int id)
    {
        var review = await _repository.GetByIdAsync(id);
        if (review == null)
        {
            return NotFound(new { message = $"Review with ID {id} not found" });
        }
        return Ok(review);
    }

    // GET: api/reviews/doctor/{doctorId}
    [HttpGet("doctor/{doctorId}")]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByDoctor(string doctorId)
    {
        var reviews = await _repository.GetByDoctorIdAsync(doctorId);
        if (reviews == null || !reviews.Any())
        {
            return Ok(new List<Review>());
        }
        return Ok(reviews);
    }

    // GET: api/reviews/rating/{rating}
    [HttpGet("rating/{rating}")]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByRating(Rating rating)
    {
        var reviews = await _repository.GetByRatingAsync(rating);
        if (reviews == null || !reviews.Any())
        {
            return Ok(new List<Review>());
        }
        return Ok(reviews);
    }

    // GET: api/reviews/date/{date}
    [HttpGet("date/{date}")]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByDate(DateOnly date)
    {
        var reviews = await _repository.GetByDateAsync(date);
        if (reviews == null || !reviews.Any())
        {
            return Ok(new List<Review>());
        }
        return Ok(reviews);
    }

    // GET: api/reviews/doctor/{doctorId}/average
    [HttpGet("doctor/{doctorId}/average")]
    public async Task<ActionResult<object>> GetAverageDoctorRating(string doctorId)
    {
        var averageRating = await _repository.GetAverageDoctorRatingAsync(doctorId);
        return Ok(new 
        { 
            doctorId = doctorId,
            averageRating = averageRating,
            outOf = 5
        });
    }

    // POST: api/reviews
    [HttpPost]
    public async Task<ActionResult<Review>> CreateReview([FromBody] ReviewCreateDto reviewDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var review = new Review
        {
            //PatientId = reviewDto.PatientId,
            DoctorId = reviewDto.DoctorId,  //fix error, make sure data type matches 
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment ?? string.Empty,
            Date = DateOnly.FromDateTime(DateTime.Now)
        };

        var createdReview = await _repository.CreateAsync(review);

        _logger.LogInformation("Review {Id} created successfully for Doctor {DoctorId}", 
            createdReview.Id, createdReview.DoctorId);

        return CreatedAtAction(
            nameof(GetReview), 
            new { id = createdReview.Id }, 
            createdReview);
    }
}