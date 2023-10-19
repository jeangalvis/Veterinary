using Domain.Entities;

namespace Domain.Interfaces;
public interface ISupplier : IGeneric<Supplier>
{
    Task<IEnumerable<Supplier>> GetSupplierxMedicine(string name);
}
