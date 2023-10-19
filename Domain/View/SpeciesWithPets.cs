using Domain.Entities;

namespace Domain.View;
public class SpeciesWithPets
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Pet> Pets { get; set; }
}
