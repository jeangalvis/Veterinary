using Domain.Entities;
using Domain.Interfaces;
using Domain.View;
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
    public async Task<IEnumerable<SoldMedicineTotal>> GetMovMedWithTotal()
    {
        return await _context.SoldMedicines
                        .Select(p => new SoldMedicineTotal
                        {
                            Id = p.Id,
                            Amount = p.Amount,
                            Price = p.Price,
                            SoldDate = p.SoldDate,
                            IdMedicinefk = p.IdMedicinefk,
                            Total = p.Price * p.Amount
                        })
                        .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<SoldMedicine> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.SoldMedicines as IQueryable<SoldMedicine>;
        if (!string.IsNullOrEmpty(search))
        {
            //query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Medicine)
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
