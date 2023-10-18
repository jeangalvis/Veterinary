namespace API.Dtos;
public class MedicineDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public string Laboratory { get; set; }
    public int IdSupplierfk { get; set; }
}
