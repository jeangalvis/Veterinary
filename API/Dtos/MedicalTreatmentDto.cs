namespace API.Dtos;
public class MedicalTreatmentDto
{
    public int Id { get; set; }
    public int Dosage { get; set; }
    public DateTime AdministrationDate { get; set; }
    public string Comment { get; set; }
    public int IdAppointmentfk { get; set; }
    public int IdMedicinefk { get; set; }
}
