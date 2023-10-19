using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class SupplierRepository : GenericRepository<Supplier>, ISupplier
{
    private readonly VeterinaryContext _context;
    public SupplierRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<Supplier> GetByIdAsync(int id)
    {
        return await _context.Suppliers
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<Supplier>> GetAllAsync()
    {
        return await _context.Suppliers
                        .ToListAsync();
    }
    public async Task<IEnumerable<Supplier>> GetSupplierxMedicine(string name)
    {
        return await _context.Suppliers
                        .Include(p => p.Medicines)
                        .Where(p => p.Medicines.Any(p => p.Name.ToLower() == name.ToLower()))
                        .ToListAsync();
    }
}
