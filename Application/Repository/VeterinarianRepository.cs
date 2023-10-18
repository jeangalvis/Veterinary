using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class VeterinarianRepository : GenericRepository<Veterinarian>, IVeterinarian
{
    private readonly VeterinaryContext _context;
    public VeterinarianRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<Veterinarian> GetByIdAsync(int id)
    {
        return await _context.Veterinarians
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<Veterinarian>> GetAllAsync()
    {
        return await _context.Veterinarians
                        .ToListAsync();
    }
}
