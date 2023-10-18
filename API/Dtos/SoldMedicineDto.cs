namespace API.Dtos;
public class SoldMedicineDto
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public DateTime SoldDate { get; set; }
    public int IdMedicinefk { get; set; }
}
