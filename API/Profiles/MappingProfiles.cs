using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.View;

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
        CreateMap<Owner, OwnersWithPetsDto>().ReverseMap();
        CreateMap<SpeciesWithPets, SpeciesWithPetsDto>().ReverseMap();
        CreateMap<SoldMedicineTotal, SoldMedicineTotalDto>().ReverseMap();
        CreateMap<Pet, PetsWithOwnerDto>().ReverseMap();
        CreateMap<BreedWithPetCount, BreedWithPetCountDto>().ReverseMap();
    }
}
