using Domain.Entities;

namespace Domain.Interfaces;
public interface IPet : IGeneric<Pet>
{
    Task<IEnumerable<Pet>> GetPetsxSpecie();
    Task<IEnumerable<Pet>> GetPetsxReason();
}
