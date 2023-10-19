using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class UserRepository : GenericRepository<User>, IUser
{
    private readonly VeterinaryContext _context;
    public UserRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public async Task<User> GetByRefreshToken(string refreshToken)
    {
        return await _context.Users
                .Include(u => u.Rols)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
    }

    public async Task<User> GetByUsername(string username)
    {
        return await _context.Users
                    .Include(u => u.Rols)
                    .Include(u => u.RefreshTokens)
                    .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }
}
