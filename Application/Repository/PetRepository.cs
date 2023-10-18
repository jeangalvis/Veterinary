using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class PetRepository : GenericRepository<Pet>, IPet
{
    private readonly VeterinaryContext _context;
    public PetRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<Pet> GetByIdAsync(int id)
    {
        return await _context.Pets
                        .Include(p => p.Owner)
                        .Include(p => p.Breed)
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<Pet>> GetAllAsync()
    {
        return await _context.Pets
                        .Include(p => p.Owner)
                        .Include(p => p.Breed)
                        .ToListAsync();
    }

    public async Task<IEnumerable<Pet>> GetPetsxSpecie()
    {
        return await _context.Pets
                        .Include(p => p.Breed)
                        .ThenInclude(p => p.Species)
                        .Where(p => p.Breed.Species.Name.ToLower() == "Felino".ToLower())
                        .ToListAsync();
    }
    public async Task<IEnumerable<Pet>> GetPetsxReason()
    {
        DateTime start = new DateTime(2023, 1, 1);
        DateTime end = start.AddMonths(3).AddDays(-1);
        return await _context.Pets
                        .Include(p => p.Appointments)
                        .Where(p => p.Appointments.Any(p => p.Reason.ToLower() == "Vacunacion".ToLower() && p.Date >= start && p.Date <= end))
                        .ToListAsync();
    }
}
