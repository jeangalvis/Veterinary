using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class MedicineRepository : GenericRepository<Medicine>, IMedicine
{
    private readonly VeterinaryContext _context;
    public MedicineRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
}
