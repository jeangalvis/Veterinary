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
}
