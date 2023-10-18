using Domain.Entities;

namespace Domain.Interfaces;
public interface IMedicine : IGeneric<Medicine>
{
    Task<IEnumerable<Medicine>> GetMedicinesxSupplier();
    Task<IEnumerable<Medicine>> GetMedicinesMoreExpensiveThan();
}
