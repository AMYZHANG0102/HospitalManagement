/* Amy Zhang
Summary: Service class that interacts with the API for shift-related operations. */

using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using HospitalManagement.BlazorServer.Models;
namespace HospitalManagement.BlazorServer.Services;

public class ShiftService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly string _baseUrl;

    public ShiftService(
        HttpClient httpClient,
        IConfiguration configuration,
        AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _baseUrl = configuration["HospitalManagement:BaseUrl"] ?? "http://localhost:5000/api/";
    }

    public async Task<List<Shift>> GetShiftsAsync()
    {
        try
        {
            // Check if user is authenticated
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            // Get user role and ID
            string? role = user.FindFirst("role")?.Value;
            string? userId = user.FindFirst("id")?.Value;

            string url = "";

            switch(role)
            {
                case "Admin":
                    url = $"{_baseUrl}/shifts";
                    break;
                case "Doctor":
                    url = $"{_baseUrl}/doctors/{userId}/shifts";
                    break;
                default:
                    throw new Exception("Unknown role");
            }

            var response = await _httpClient.GetFromJsonAsync<List<Shift>>(url);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: ", ex);
            return null;
        }
    }

    public async Task<Shift?> GetShiftAsync(int id)
    {
        try
        {
            var response = await _httpClient
                                 .GetFromJsonAsync<Shift>($"{_baseUrl}/shifts/{id}");
            return response;                    
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: ", ex);
            return null;
        }
    }

    public async Task<bool> CreateShiftAsync(Shift shift)
    {
        try {
            var response = await _httpClient
                                .PostAsJsonAsync($"{_baseUrl}/shifts", shift);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: ", ex);
            return false;
        }         
    }

    public async Task<bool> UpdateShiftAsync(Shift shift)
    {
        try {
            var response = await _httpClient
                                .PutAsJsonAsync($"{_baseUrl}/shifts/{shift.Id}", shift);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: ", ex);
            return false;
        }         
    }
}