namespace API.Dtos;
public class SpeciesWithPetsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PetDto> Pets { get; set; }
}
