using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class PurchasedMedicineRepository : GenericRepository<PurchasedMedicine>, IPurchasedMedicine
{
    private readonly VeterinaryContext _context;
    public PurchasedMedicineRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<PurchasedMedicine> GetByIdAsync(int id)
    {
        return await _context.purchasedMedicines
                        .Include(p => p.Supplier)
                        .Include(p => p.Medicine)
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<PurchasedMedicine>> GetAllAsync()
    {
        return await _context.purchasedMedicines
                        .Include(p => p.Supplier)
                        .Include(p => p.Medicine)
                        .ToListAsync();
    }
}
