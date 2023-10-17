using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class RolRepository : GenericRepository<Rol>, IRol
{
    private readonly VeterinaryContext _context;
    public RolRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
}
