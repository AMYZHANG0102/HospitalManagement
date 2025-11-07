using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Doctor : User
{
    [Required (ErrorMessage = "Specialization must be stated")]
    public Specialization Specialization { get; set; }

    public List<Appointment> Appointments { get; set; } = new(); // Initially empty

    public List<Shift> Shifts { get; set; } = new();

    public List<Feedback> FeedbackReceived { get; set; } = new();
}