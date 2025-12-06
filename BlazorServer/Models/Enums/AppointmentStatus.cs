/* Amy Zhang
Summary: This enum defines the different statuses for appointments. */

namespace HospitalManagement.BlazorServer.Models;

public enum AppointmentStatus
{
    Pending = 0,
    Completed = 1,
    Canceled = 2,
}