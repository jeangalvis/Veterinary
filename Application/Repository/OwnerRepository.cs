using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class OwnerRepository : GenericRepository<Owner>, IOwner
{
    private readonly VeterinaryContext _context;
    public OwnerRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
}
