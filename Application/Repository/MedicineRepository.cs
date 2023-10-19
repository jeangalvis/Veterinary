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
    public async Task<IEnumerable<Medicine>> GetMedicinesxSupplier()
    {
        return await _context.Medicines
                    .Include(p => p.Supplier).Where(p => p.Supplier.Name.ToLower() == "Genfar".ToLower())
                    .ToListAsync();
    }

    public async Task<IEnumerable<Medicine>> GetMedicinesMoreExpensiveThan()
    {
        return await _context.Medicines
                            .Include(p => p.Supplier)
                            .Where(p => p.Price > 50000)
                            .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Medicines as IQueryable<Medicine>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Supplier)
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
    public async Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetMedicinesxSupplier(int pageIndex, int pageSize, string search)
    {
        var query = _context.Medicines as IQueryable<Medicine>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Supplier).Where(p => p.Supplier.Name.ToLower() == "Genfar".ToLower())
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
    public async Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetMedicinesMoreExpensiveThan(int pageIndex, int pageSize, string search)
    {
        var query = _context.Medicines as IQueryable<Medicine>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Supplier)
                                .Where(p => p.Price > 50000)
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
