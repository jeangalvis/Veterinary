using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class SoldMedicineRepository : GenericRepository<SoldMedicine>, ISoldMedicine
{
    private readonly VeterinaryContext _context;
    public SoldMedicineRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<SoldMedicine> GetByIdAsync(int id)
    {
        return await _context.SoldMedicines
                        .Include(p => p.Medicine)
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<SoldMedicine>> GetAllAsync()
    {
        return await _context.SoldMedicines
                        .Include(p => p.Medicine)
                        .ToListAsync();
    }
}
