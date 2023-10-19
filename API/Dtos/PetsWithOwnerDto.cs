namespace API.Dtos;
public class PetsWithOwnerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public int IdOwnerfk { get; set; }
    public int IdBreedfk { get; set; }
    public OwnerDto Owner { get; set; }
}
