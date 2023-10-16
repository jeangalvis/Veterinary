namespace Domain.Entities;
public class Medicine : BaseEntity
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public string Laboratory { get; set; }
    public int IdSupplierfk { get; set; }
    public Supplier Supplier { get; set; }
    public ICollection<SoldMedicine> SoldMedicines { get; set; }
    public ICollection<MedicalTreatment> MedicalTreatments { get; set; }
    public ICollection<PurchasedMedicine> PurchasedMedicines { get; set; }

}
