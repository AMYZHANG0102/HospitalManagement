/* Amy Zhang
Summary: This enum defines the various types of appointments available
between patients and doctors. */

namespace HospitalManagement.Core.Models;

public enum AppointmentType
{
    General = 0,
    Procedure = 1,
    Followup = 2,
    Preventive = 3,
    Diagnostic = 4,
    Screening = 5,
    Acute = 6,
    Specialist = 7
}