using Domain.Entities;
using Domain.View;

namespace Domain.Interfaces;
public interface IPet : IGeneric<Pet>
{
    Task<IEnumerable<Pet>> GetPetsxSpecie();
    Task<IEnumerable<Pet>> GetPetsxReason();
    Task<IEnumerable<SpeciesWithPets>> GetPetsGroupBySpecie();
    Task<IEnumerable<Pet>> GetPetsxVeterinarian(string name);
    Task<IEnumerable<Pet>> GetPetsGoldenRetriever();
    Task<IEnumerable<BreedWithPetCount>> GetPetCountByBreed();
    Task<(int totalRegistros, IEnumerable<Pet> registros)> GetPetsxSpecie(int pageIndex, int pageSize, string search);
    Task<(int totalRegistros, IEnumerable<Pet> registros)> GetPetsxReason(int pageIndex, int pageSize, string search);
    Task<(int totalRegistros, IEnumerable<SpeciesWithPets> registros)> GetPetsGroupBySpecie(int pageIndex, int pageSize, string search);
    Task<(int totalRegistros, IEnumerable<Pet> registros)> GetPetsxVeterinarian(int pageIndex, int pageSize, string search, string name);
}
