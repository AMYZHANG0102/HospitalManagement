/* Amy Zhang
Summary: This enum defines the various types of appointments available
between patients and doctors. */

namespace HospitalManagement.Core.Models;

public enum AppointmentType
{
    General,
    Procedure,
    Followup,
    Preventive,
    Diagnostic,
    Screening,
    Acute,
    Specialist
}