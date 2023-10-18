using Domain.Entities;

namespace Domain.Interfaces;
public interface IVeterinarian : IGeneric<Veterinarian>
{
    Task<IEnumerable<Veterinarian>> GetVeterinarianxSpeaciality();
}
