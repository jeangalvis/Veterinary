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
    public override async Task<(int totalRegistros, IEnumerable<MedicalTreatment> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.MedicalTreatments as IQueryable<MedicalTreatment>;
        if (!string.IsNullOrEmpty(search))
        {
            //query = query.Where(p => p.Comment.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Appointment)
                                .Include(p => p.Medicine)
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
