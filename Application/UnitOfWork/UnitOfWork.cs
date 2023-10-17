using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly VeterinaryContext context;
    public IAppointment _appointment;
    public IBreed _breed;
    public IMedicalTreatment _medicalTreatment;
    public IMedicine _medicine;
    public IOwner _owner;
    public IPet _pet;
    public IPurchasedMedicine _purchasedMedicine;
    public IRol _rol;
    public ISoldMedicine _soldMedicine;
    public ISpecies _species;
    public ISupplier _supplier;
    public IUser _user;
    public IVeterinarian _veterinarian;
    public UnitOfWork(VeterinaryContext _context)
    {
        context = _context;
    }
    public IAppointment Appointments
    {
        get
        {
            if (_appointment == null)
            {
                _appointment = new AppointmentRepository(context);
            }
            return _appointment;
        }
    }

    public IBreed Breeds
    {
        get
        {
            if (_breed == null)
            {
                _breed = new BreedRepository(context);
            }
            return _breed;
        }
    }

    public IMedicalTreatment MedicalTreatments
    {
        get
        {
            if (_medicalTreatment == null)
            {
                _medicalTreatment = new MedicalTreatmentRepository(context);
            }
            return _medicalTreatment;
        }
    }

    public IMedicine Medicines
    {
        get
        {
            if (_medicine == null)
            {
                _medicine = new MedicineRepository(context);
            }
            return _medicine;
        }
    }

    public IOwner Owners
    {
        get
        {
            if (_owner == null)
            {
                _owner = new OwnerRepository(context);
            }
            return _owner;
        }
    }

    public IPet Pets
    {
        get
        {
            if (_pet == null)
            {
                _pet = new PetRepository(context);
            }
            return _pet;
        }
    }

    public IPurchasedMedicine PurchasedMedicine
    {
        get
        {
            if (_purchasedMedicine == null)
            {
                _purchasedMedicine = new PurchasedMedicineRepository(context);
            }
            return _purchasedMedicine;
        }
    }

    public IRol Rols
    {
        get
        {
            if (_rol == null)
            {
                _rol = new RolRepository(context);
            }
            return _rol;
        }
    }

    public ISoldMedicine SoldMedicines
    {
        get
        {
            if (_soldMedicine == null)
            {
                _soldMedicine = new SoldMedicineRepository(context);
            }
            return _soldMedicine;
        }
    }

    public ISpecies Species
    {
        get
        {
            if (_species == null)
            {
                _species = new SpeciesRepository(context);
            }
            return _species;
        }
    }

    public ISupplier Suppliers
    {
        get
        {
            if (_supplier == null)
            {
                _supplier = new SupplierRepository(context);
            }
            return _supplier;
        }
    }

    public IUser Users
    {
        get
        {
            if (_user == null)
            {
                _user = new UserRepository(context);
            }
            return _user;
        }
    }

    public IVeterinarian Veterinarians
    {
        get
        {
            if (_veterinarian == null)
            {
                _veterinarian = new VeterinarianRepository(context);
            }
            return _veterinarian;
        }
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync();
    }
}
