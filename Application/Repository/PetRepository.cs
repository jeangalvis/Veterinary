using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class PetRepository : GenericRepository<Pet>, IPet
{
    private readonly VeterinaryContext _context;
    public PetRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
}
