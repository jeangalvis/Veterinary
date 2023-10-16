namespace Domain.Entities;
public class Owner : BaseEntity
{
    public string Name { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public ICollection<Pet> Pets { get; set; }
}
