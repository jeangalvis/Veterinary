using Domain.Entities;
using Domain.View;

namespace Domain.Interfaces;
public interface ISoldMedicine : IGeneric<SoldMedicine>
{
    Task<IEnumerable<SoldMedicineTotal>> GetMovMedWithTotal();
    Task<(int totalRegistros, IEnumerable<SoldMedicineTotal> registros)> GetMovMedWithTotal(int pageIndex, int pageSize, string search);
}
