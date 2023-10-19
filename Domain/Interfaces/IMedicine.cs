using Domain.Entities;

namespace Domain.Interfaces;
public interface IMedicine : IGeneric<Medicine>
{
    Task<IEnumerable<Medicine>> GetMedicinesxSupplier();
    Task<IEnumerable<Medicine>> GetMedicinesMoreExpensiveThan();
    Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetMedicinesxSupplier(int pageIndex, int pageSize, string search);
    Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetMedicinesMoreExpensiveThan(int pageIndex, int pageSize, string search);
}
