using Domain.Entities;
using Domain.Interfaces;
using Domain.View;
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
    public async Task<IEnumerable<SpeciesWithPets>> GetPetsGroupBySpecie()
    {
        var groupedPets = await _context.Pets
                                .Include(p => p.Breed)
                                    .ThenInclude(b => b.Species)
                                .ToListAsync();

        var result = groupedPets.GroupBy(p => p.Breed.Species)
                                .Select(group => new SpeciesWithPets
                                {
                                    Id = group.Key.Id,
                                    Name = group.Key.Name,
                                    Pets = group.ToList()
                                });

        return result;
    }
    public async Task<IEnumerable<Pet>> GetPetsxVeterinarian(string name)
    {
        return await _context.Pets
                .Include(p => p.Appointments)
                .ThenInclude(p => p.Veterinarian)
                .Where(p => p.Appointments.Any(p => p.Veterinarian.Name.ToLower() == name.ToLower()))
                .ToListAsync();
    }
    public async Task<IEnumerable<Pet>> GetPetsGoldenRetriever()
    {
        return await _context.Pets
                .Include(p => p.Breed)
                .Include(p => p.Owner)
                .Where(p => p.Breed.Name.ToLower() == "Golden Retriever".ToLower())
                .ToListAsync();
    }
    public async Task<IEnumerable<BreedWithPetCount>> GetPetCountByBreed()
    {
        var breedCounts = await _context.Pets
                                    .GroupBy(p => p.Breed.Name)
                                    .Select(group => new BreedWithPetCount
                                    {
                                        BreedName = group.Key,
                                        PetCount = group.Count()
                                    })
                                    .ToListAsync();

        return breedCounts;
    }
}
