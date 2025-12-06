/* Amy Zhang
Summary: Service class that interacts with the API for appointment-related operations. */

using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using HospitalManagement.BlazorServer.Models;
namespace HospitalManagement.BlazorServer.Services;

public class AppointmentService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly string _baseUrl;

    public AppointmentService(
        HttpClient httpClient,
        IConfiguration configuration,
        AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _baseUrl = configuration["HospitalManagement:BaseUrl"] ?? "http://localhost:5000/api/";
    }

    public async Task<List<Appointment>> GetAppointmentsAsync()
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
                    url = $"{_baseUrl}/appointments";
                    break;
                case "Doctor":
                    url = $"{_baseUrl}/doctors/{userId}/appointments";
                    break;
                case "Patient":
                    url = $"{_baseUrl}/patients/{userId}/appointments";
                    break;
                default:
                    throw new Exception("Unknown role");
            }

            var response = await _httpClient.GetFromJsonAsync<List<Appointment>>(url);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: ", ex);
            return null;
        }
    }

    public async Task<List<Appointment>> GetAppointmentsWhereDocIsUnavalable()
    {
        try
        {
            string url = $"{_baseUrl}/appointments/unavailabledoctor";
            var response = await _httpClient.GetFromJsonAsync<List<Appointment>>(url);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: ", ex);
            return null;
        }
    }
}