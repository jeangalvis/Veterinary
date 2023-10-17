using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class SpeciesRepository : GenericRepository<Species>, ISpecies
{
    private readonly VeterinaryContext _context;
    public SpeciesRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
}
