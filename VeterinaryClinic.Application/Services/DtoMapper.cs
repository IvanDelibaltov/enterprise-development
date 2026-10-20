using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Domain.Models;

namespace VeterinaryClinic.Application.Services;

public static class DtoMapper
{
    public static BreedDto Map(Breed x) => new(x.Id, x.Name, x.AnimalType);
    public static SpecializationDto Map(Specialization x) => new(x.Id, x.Name, x.AnimalType);
    public static OwnerDto Map(Owner x) => new(x.Id, x.LastName, x.FirstName, x.Patronymic, x.Phone, x.Address);
    public static VeterinarianDto Map(Veterinarian x) => new(x.Id, x.LastName, x.FirstName, x.Patronymic, x.Phone, x.PassportNumber, x.BirthDate, x.Specialization.Id, x.ExperienceYears);
    public static PetDto Map(Pet x) => new(x.Id, x.Name, x.Breed.Id, x.BirthDate, x.Weight, x.Owner?.Id);
    public static AppointmentDto Map(Appointment x) => new(x.Id, x.Pet.Id, x.Veterinarian.Id, x.AppointmentDate, x.RoomNumber, x.IsRepeated);

}
