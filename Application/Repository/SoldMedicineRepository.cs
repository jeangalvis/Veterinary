using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class SoldMedicineRepository : GenericRepository<SoldMedicine>, ISoldMedicine
{
    private readonly VeterinaryContext _context;
    public SoldMedicineRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
}
