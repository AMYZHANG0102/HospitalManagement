/* Amy Zhang
Summary: This child class represents a doctor entity.
It inherits from the User class and has additional doctor-related fields. */

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace HospitalManagement.Core.Models;

public class Doctor : User
{
    [Required (ErrorMessage = "Specialization must be stated")]
    public Specialization Specialization { get; set; }

    [JsonIgnore]
    public List<Shift> Shifts { get; set; } = new();

    [JsonIgnore]
    public List<Appointment> Appointments { get; set; } = new(); // Initially empty

    [JsonIgnore]
    public List<Review> ReviewsReceived { get; set; } = new();
}