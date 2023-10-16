namespace Domain.Entities;
public class UserRol
{
    public int IdUserfk { get; set; }
    public User Usuario { get; set; }
    public int IdRolfk { get; set; }
    public Rol Rol { get; set; }
}
