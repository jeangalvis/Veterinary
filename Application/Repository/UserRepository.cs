using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class UserRepository : GenericRepository<User>, IUser
{
    private readonly VeterinaryContext _context;
    public UserRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
}