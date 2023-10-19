using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class BreedRepository : GenericRepository<Breed>, IBreed
{
    private readonly VeterinaryContext _context;
    public BreedRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<Breed> GetByIdAsync(int id)
    {
        return await _context.Breeds
                        .Include(p => p.Species)
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<Breed>> GetAllAsync()
    {
        return await _context.Breeds
                        .Include(p => p.Species)
                        .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Breed> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Breeds as IQueryable<Breed>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Species)
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
