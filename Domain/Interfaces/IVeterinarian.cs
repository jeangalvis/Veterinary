using Domain.Entities;

namespace Domain.Interfaces;
public interface IVeterinarian : IGeneric<Veterinarian>
{
    Task<IEnumerable<Veterinarian>> GetVeterinarianxSpeaciality();
    Task<(int totalRegistros, IEnumerable<Veterinarian> registros)> GetVeterinarianxSpeaciality(int pageIndex, int pageSize, string search);
}
