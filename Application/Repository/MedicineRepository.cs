using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class MedicineRepository : GenericRepository<Medicine>, IMedicine
{
    private readonly VeterinaryContext _context;
    public MedicineRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<Medicine> GetByIdAsync(int id)
    {
        return await _context.Medicines
                        .Include(p => p.Supplier)
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<Medicine>> GetAllAsync()
    {
        return await _context.Medicines
                        .Include(p => p.Supplier)
                        .ToListAsync();
    }
}
