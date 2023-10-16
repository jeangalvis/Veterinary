namespace Domain.Entities;
public class Veterinarian : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Speciality { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}
