using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class RolRepository : GenericRepository<Rol>, IRol
{
    private readonly VeterinaryContext _context;
    public RolRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<Rol> GetByIdAsync(int id)
    {
        return await _context.Rols
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<Rol>> GetAllAsync()
    {
        return await _context.Rols
                        .ToListAsync();
    }
}
