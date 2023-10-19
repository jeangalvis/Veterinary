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
    public override async Task<(int totalRegistros, IEnumerable<Supplier> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Suppliers as IQueryable<Supplier>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
    public async Task<(int totalRegistros, IEnumerable<Supplier> registros)> GetSupplierxMedicine(int pageIndex, int pageSize, string search, string name)
    {
        var query = _context.Suppliers as IQueryable<Supplier>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Medicines)
                                .Where(p => p.Medicines.Any(p => p.Name.ToLower() == name.ToLower()))
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
