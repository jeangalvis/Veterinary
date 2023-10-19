using Domain.Entities;

namespace Domain.Interfaces;
public interface IOwner : IGeneric<Owner>
{
    Task<IEnumerable<Owner>> GetOwnersWithPets();
    Task<(int totalRegistros, IEnumerable<Owner> registros)> GetOwnersWithPets(int pageIndex, int pageSize, string search);
}
