namespace API.Dtos;
public class OwnersWithPetsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public List<PetDto> Pets { get; set; }
}
