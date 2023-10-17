using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class MedicalTreatmentRepository : GenericRepository<MedicalTreatment>, IMedicalTreatment
{
    private readonly VeterinaryContext _context;
    public MedicalTreatmentRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
}
