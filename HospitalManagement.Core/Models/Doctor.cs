/* Amy Zhang
Summary: This child class represents a doctor entity.
It inherits from the User class and has additional doctor-related fields. */

using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Doctor : User
{
    [Required (ErrorMessage = "Specialization must be stated")]
    public Specialization Specialization { get; set; }

    public List<Shift> Shifts { get; set; } = new();

    public List<Appointment> Appointments { get; set; } = new(); // Initially empty

    public List<Review> ReviewsReceived { get; set; } = new();
}