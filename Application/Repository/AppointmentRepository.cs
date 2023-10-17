using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class AppointmentRepository : GenericRepository<Appointment>, IAppointment
{
    private readonly VeterinaryContext _context;
    public AppointmentRepository(VeterinaryContext context) : base(context)
    {
        _context = context;
    }
}
