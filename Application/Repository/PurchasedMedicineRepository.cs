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
    public override async Task<(int totalRegistros, IEnumerable<PurchasedMedicine> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.purchasedMedicines as IQueryable<PurchasedMedicine>;
        if (!string.IsNullOrEmpty(search))
        {
            //query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Supplier)
                                .Include(p => p.Medicine)
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
