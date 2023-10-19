using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class OwnerRepository : GenericRepository<Owner>, IOwner
{
    private readonly VeterinaryContext _context;
    public OwnerRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<Owner> GetByIdAsync(int id)
    {
        return await _context.Owners
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<Owner>> GetAllAsync()
    {
        return await _context.Owners
                        .ToListAsync();
    }
    public async Task<IEnumerable<Owner>> GetOwnersWithPets()
    {
        return await _context.Owners
                        .Include(p => p.Pets)
                        .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Owner> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Owners as IQueryable<Owner>;
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
