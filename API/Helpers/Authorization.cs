namespace API.Helpers;
public class Authorization
{
    public enum Roles
    {
        Administrator,
        Employee
    }
    public const Roles rol_default = Roles.Employee;
}
