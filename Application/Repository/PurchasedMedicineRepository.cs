using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class PurchasedMedicineRepository : GenericRepository<PurchasedMedicine>, IPurchasedMedicine
{
    private readonly VeterinaryContext _context;
    public PurchasedMedicineRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
}
