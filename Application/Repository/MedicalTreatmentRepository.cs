using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class MedicalTreatmentRepository : GenericRepository<MedicalTreatment>, IMedicalTreatment
{
    private readonly VeterinaryContext _context;
    public MedicalTreatmentRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<MedicalTreatment> GetByIdAsync(int id)
    {
        return await _context.MedicalTreatments
                        .Include(p => p.Appointment)
                        .Include(p => p.Medicine)
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<MedicalTreatment>> GetAllAsync()
    {
        return await _context.MedicalTreatments
                        .Include(p => p.Appointment)
                        .Include(p => p.Medicine)
                        .ToListAsync();
    }
}
