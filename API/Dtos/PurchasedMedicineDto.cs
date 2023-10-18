namespace API.Dtos;
public class PurchasedMedicineDto
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchasedDate { get; set; }
    public int IdSupplierfk { get; set; }
    public int IdMedicinefk { get; set; }
}
