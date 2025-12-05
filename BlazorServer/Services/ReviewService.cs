/*Iram*/
using System.Net.Http.Json;
using HospitalManagement.BlazorServer.Models;

public class ReviewService
{
    private readonly HttpClient _http;

    public ReviewsService(HttpClient http)
    {
        _http = http;
    }

    // GET: /api/reviews
    public async Task<IEnumerable<Review>?> GetReviewsAsync()
    {
        var resp = await _http.GetAsync("api/reviews");
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<IEnumerable<Review>>();
    }

    // GET: /api/reviews/doctor/{doctorId}
    public async Task<IEnumerable<Review>?> GetReviewsByDoctorAsync(string doctorId)
    {
        var resp = await _http.GetAsync($"api/reviews/doctor/{Uri.EscapeDataString(doctorId)}");
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<IEnumerable<Review>>();
    }

    // POST: /api/reviews
    public async Task<Review?> CreateReviewAsync(object reviewCreateDto)
    {
        var resp = await _http.PostAsJsonAsync("api/reviews", reviewCreateDto);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<Review>();
    }
}
