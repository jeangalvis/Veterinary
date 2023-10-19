using Domain.Entities;

namespace Domain.Interfaces;
public interface ISupplier : IGeneric<Supplier>
{
    Task<IEnumerable<Supplier>> GetSupplierxMedicine(string name);
    Task<(int totalRegistros, IEnumerable<Supplier> registros)> GetSupplierxMedicine(int pageIndex, int pageSize, string search, string name);
}
