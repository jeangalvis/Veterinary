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

}
