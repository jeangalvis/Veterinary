using Domain.Entities;

namespace Domain.Interfaces;
public interface IUser : IGeneric<User>
{
    Task<User> GetByUsername(string username);
    Task<User> GetByRefreshToken(string refreshToken);
}
