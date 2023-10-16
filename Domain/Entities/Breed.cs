namespace Domain.Entities;
public class Breed : BaseEntity
{
    public string Name { get; set; }
    public int IdSpeciesfk { get; set; }
    public Species Species { get; set; }
    public ICollection<Pet> Pets { get; set; }
}
