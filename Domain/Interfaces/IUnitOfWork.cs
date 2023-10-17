namespace Domain.Interfaces;
public interface IUnitOfWork
{
    IAppointment Appointments { get; }
    IBreed Breeds { get; }
    IMedicalTreatment MedicalTreatments { get; }
    IMedicine Medicines { get; }
    IOwner Owners { get; }
    IPet Pets { get; }
    IPurchasedMedicine PurchasedMedicine { get; }
    IRol Rols { get; }
    ISoldMedicine SoldMedicines { get; }
    ISpecies Species { get; }
    ISupplier Suppliers { get; }
    IUser Users { get; }
    IVeterinarian Veterinarians { get; }
    Task<int> SaveAsync();

}
