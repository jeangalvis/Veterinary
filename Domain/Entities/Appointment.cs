namespace Domain.Entities;
public class Appointment : BaseEntity
{
    public DateTime Date { get; set; }
    public string Reason { get; set; }
    public int IdPetfk { get; set; }
    public Pet Pet { get; set; }
    public int IdVeterinarianfk { get; set; }
    public Veterinarian Veterinarian { get; set; }
    public ICollection<MedicalTreatment> MedicalTreatments { get; set; }
}
