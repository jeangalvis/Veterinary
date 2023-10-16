namespace Domain.Entities;
public class MedicalTreatment : BaseEntity
{
    public int Dosage { get; set; }
    public DateTime AdministrationDate { get; set; }
    public string Comment { get; set; }
    public int IdAppointmentfk { get; set; }
    public Appointment Appointment { get; set; }
    public int IdMedicinefk { get; set; }
    public Medicine Medicine { get; set; }
}
