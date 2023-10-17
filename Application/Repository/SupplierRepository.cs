using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class SupplierRepository : GenericRepository<Supplier>, ISupplier
{
    private readonly VeterinaryContext _context;
    public SupplierRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
}
