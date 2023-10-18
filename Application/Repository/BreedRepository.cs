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
}
