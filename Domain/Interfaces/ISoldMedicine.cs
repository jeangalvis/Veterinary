using Domain.Entities;
using Domain.View;

namespace Domain.Interfaces;
public interface ISoldMedicine : IGeneric<SoldMedicine>
{
    Task<IEnumerable<SoldMedicineTotal>> GetMovMedWithTotal();
}
