using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class BreedRepository : GenericRepository<Breed>, IBreed
{
    private readonly VeterinaryContext _context;
    public BreedRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
}
