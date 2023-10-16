namespace Domain.Entities;
public class PurchasedMedicine : BaseEntity
{
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchasedDate { get; set; }
    public int IdSupplierfk { get; set; }
    public Supplier Supplier { get; set; }
    public int IdMedicinefk { get; set; }
    public Medicine Medicine { get; set; }
}
