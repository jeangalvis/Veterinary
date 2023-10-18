using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class PetRepository : GenericRepository<Pet>, IPet
{
    private readonly VeterinaryContext _context;
    public PetRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<Pet> GetByIdAsync(int id)
    {
        return await _context.Pets
                        .Include(p => p.Owner)
                        .Include(p => p.Breed)
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<Pet>> GetAllAsync()
    {
        return await _context.Pets
                        .Include(p => p.Owner)
                        .Include(p => p.Breed)
                        .ToListAsync();
    }
}
