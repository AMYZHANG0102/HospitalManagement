using HospitalManagement.Core.Models;

public class AppointmentReadDto
{
    public int Id { get; set; }

    public string PatientId { get; set; }

    public string PatientName { get; set; }

    public string DoctorId { get; set; }

    public string DoctorName { get; set; }
    
    public AppointmentType AppType { get; set; }

    public DateTime DateTime { get; set; }

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

    public bool DoctorIsUnavailable { get; set; } = false;
}