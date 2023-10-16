namespace Domain.Entities;
public class Pet : BaseEntity
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public int IdOwnerfk { get; set; }
    public Owner Owner { get; set; }
    public int IdBreedfk { get; set; }
    public Breed Breed { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}
