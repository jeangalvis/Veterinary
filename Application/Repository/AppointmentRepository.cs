using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class AppointmentRepository : GenericRepository<Appointment>, IAppointment
{
    private readonly VeterinaryContext _context;
    public AppointmentRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<Appointment> GetByIdAsync(int id)
    {
        return await _context.Appointments
                        .Include(p => p.Pet)
                        .Include(p => p.Veterinarian)
                        .FirstOrDefaultAsync(p => p.Id == id);

    }

    public override async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
                        .Include(p => p.Pet)
                        .Include(p => p.Veterinarian)
                        .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Appointment> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Appointments as IQueryable<Appointment>;
        if (!string.IsNullOrEmpty(search))
        {
            //query = query.Where(p => p.Reason.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Pet)
                                .Include(p => p.Veterinarian)
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
