public class ShiftReadDto
{
    public int Id { get; set; }

    public string DoctorId { get; set; }
    
    public string DoctorName { get; set; }
    
    public DateTime StartDateTime { get; set; }
    
    public DateTime EndDateTime { get; set; }
}