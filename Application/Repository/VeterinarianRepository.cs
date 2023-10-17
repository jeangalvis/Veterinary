using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class VeterinarianRepository : GenericRepository<Veterinarian>, IVeterinarian
{
    private readonly VeterinaryContext _context;
    public VeterinarianRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
}
