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
    public override async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
                        .ToListAsync();
    }
}
