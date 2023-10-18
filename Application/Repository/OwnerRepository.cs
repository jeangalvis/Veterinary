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
}
