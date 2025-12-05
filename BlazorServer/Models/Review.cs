/*Iram*/
namespace HospitalManagement.BlazorServer.Models;

public class Review
{
    public int Id { get; set; }

    public string PatientId { get; set; }

    public string? DoctorId { get; set; } 

    public Rating Rating { get; set; }

    public string? Comment { get; set; } = string.Empty;
    
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

}




