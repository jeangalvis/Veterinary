using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Appointment, AppointmentDto>().ReverseMap();
        CreateMap<Breed, BreedDto>().ReverseMap();
        CreateMap<Owner, OwnerDto>().ReverseMap();
        CreateMap<Pet, PetDto>().ReverseMap();
        CreateMap<Species, SpeciesDto>().ReverseMap();
        CreateMap<Veterinarian, VeterinarianDto>().ReverseMap();
        CreateMap<MedicalTreatment, MedicalTreatmentDto>().ReverseMap();
        CreateMap<Medicine, MedicineDto>().ReverseMap();
        CreateMap<PurchasedMedicine, PurchasedMedicineDto>().ReverseMap();
        CreateMap<SoldMedicine, SoldMedicineDto>().ReverseMap();
        CreateMap<Supplier, SupplierDto>().ReverseMap();
    }
}
