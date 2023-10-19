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
    public override async Task<(int totalRegistros, IEnumerable<Pet> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Pets as IQueryable<Pet>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Owner)
                                .Include(p => p.Breed)
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
    public async Task<(int totalRegistros, IEnumerable<Pet> registros)> GetPetsxSpecie(int pageIndex, int pageSize, string search)
    {
        var query = _context.Pets as IQueryable<Pet>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Breed)
                                .ThenInclude(p => p.Species)
                                .Where(p => p.Breed.Species.Name.ToLower() == "Felino".ToLower())
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
    public async Task<(int totalRegistros, IEnumerable<Pet> registros)> GetPetsxReason(int pageIndex, int pageSize, string search)
    {
        DateTime start = new DateTime(2023, 1, 1);
        DateTime end = start.AddMonths(3).AddDays(-1);
        var query = _context.Pets as IQueryable<Pet>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Appointments)
                                .Where(p => p.Appointments.Any(p => p.Reason.ToLower() == "Vacunacion".ToLower() && p.Date >= start && p.Date <= end))
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
    public async Task<(int totalRegistros, IEnumerable<SpeciesWithPets> registros)> GetPetsGroupBySpecie(int pageIndex, int pageSize, string search)
    {
        var query = _context.Pets
                .Include(p => p.Breed)
                    .ThenInclude(b => b.Species)
                .GroupBy(p => p.Breed.Species)
                .Select(group => new SpeciesWithPets
                {
                    Id = group.Key.Id,
                    Name = group.Key.Name,
                    Pets = group.ToList()
                })
                .AsQueryable();
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
    public async Task<(int totalRegistros, IEnumerable<Pet> registros)> GetPetsxVeterinarian(int pageIndex, int pageSize, string search, string name)
    {
        var query = _context.Pets as IQueryable<Pet>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Appointments)
                                .ThenInclude(p => p.Veterinarian)
                                .Where(p => p.Appointments.Any(p => p.Veterinarian.Name.ToLower() == name.ToLower()))
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
    public async Task<(int totalRegistros, IEnumerable<Pet> registros)> GetPetsGoldenRetriever(int pageIndex, int pageSize, string search)
    {
        var query = _context.Pets as IQueryable<Pet>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Breed)
                                .Include(p => p.Owner)
                                .Where(p => p.Breed.Name.ToLower() == "Golden Retriever".ToLower())
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
    public async Task<(int totalRegistros, IEnumerable<BreedWithPetCount> registros)> GetPetCountByBreed(int pageIndex, int pageSize, string search)
    {
        var query = _context.Pets.GroupBy(p => p.Breed.Name)
                                    .Select(group => new BreedWithPetCount
                                    {
                                        BreedName = group.Key,
                                        PetCount = group.Count()
                                    }).AsQueryable();
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.BreedName.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query

                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
