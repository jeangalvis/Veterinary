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
}
