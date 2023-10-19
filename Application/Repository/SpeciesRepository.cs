using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class SpeciesRepository : GenericRepository<Species>, ISpecies
{
    private readonly VeterinaryContext _context;
    public SpeciesRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<Species> GetByIdAsync(int id)
    {
        return await _context.Species
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<Species>> GetAllAsync()
    {
        return await _context.Species
                        .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Species> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Species as IQueryable<Species>;
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
}
