namespace Domain.Entities;
public class SoldMedicine : BaseEntity
{
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public DateTime SoldDate { get; set; }
    public int IdMedicinefk { get; set; }
    public Medicine Medicine { get; set; }

}
