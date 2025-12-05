/* Amy Zhang
Summary: Model for shift, represents the shift entity received in the API */

namespace HospitalManagement.BlazorServer.Models;

public class ShiftModel
{
    public int Id { get; set; }

    public int DoctorId { get; set; }

    public DateTime StartDateTime { get; set; }
    
    public DateTime EndDateTime { get; set; }
}