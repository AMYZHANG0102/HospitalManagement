/* Amy Zhang
Summary: This class represents appointment entity received from API */

namespace BlazorServer.Models;

public class Appointment
{
    public int Id { get; set; }

    public string PatientId { get; set; }

    public string DoctorId { get; set; }
    
    // public AppointmentType Type { get; set; }

    public DateTime DateTime { get; set; }

    // public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

    // Doctor is initially available, since the system only allows users to schedule appointments with available doctors.
    // But doctors may mark themselves as unavalable for an appointment later on, which changes this property to true.
    // The admin resolves doctor unavailability by re-assigning another docotr for the patient in the admin interface.
    public bool DoctorIsUnavailable { get; set; } = false;
}