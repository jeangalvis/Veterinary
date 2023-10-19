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

    public async Task<IEnumerable<Veterinarian>> GetVeterinarianxSpeaciality()
    {
        return await _context.Veterinarians
                        .Where(p => p.Speciality.ToLower() == "cirujano vascular".ToLower())
                        .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Veterinarian> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Veterinarians as IQueryable<Veterinarian>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
