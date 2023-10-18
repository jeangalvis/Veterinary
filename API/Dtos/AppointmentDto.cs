namespace API.Dtos;
public class AppointmentDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Reason { get; set; }
    public int IdPetfk { get; set; }
    public int IdVeterinarianfk { get; set; }
}
