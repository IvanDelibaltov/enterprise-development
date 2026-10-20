using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

public sealed class VeterinarianService(IRepository<Veterinarian> veterinarians, IRepository<Specialization> specializations, IRepository<Appointment> appointments)
{
    public IReadOnlyList<VeterinarianDto> Veterinarians() => veterinarians.GetAll().Select(DtoMapper.Map).ToArray();
    public IReadOnlyList<VeterinarianDto> VeterinariansBySpecies(AnimalType species) => veterinarians.GetAll()
        .Where(x => x.Specialization.AnimalType == species).Select(DtoMapper.Map).ToArray();
    public IReadOnlyList<PetDto> PetsByVeterinarian(int id) => appointments.GetAll()
        .Where(x => x.Veterinarian.Id == id).Select(x => x.Pet).DistinctBy(x => x.Id)
        .OrderBy(x => x.Name).Select(DtoMapper.Map).ToArray();
    public VeterinarianDto Save(int? id, SaveVeterinarianRequest x)
    {
        var spec = specializations.Get(x.SpecializationId) ?? throw new ArgumentException("Specialization not found");
        if (string.IsNullOrWhiteSpace(x.FirstName) || string.IsNullOrWhiteSpace(x.LastName) || string.IsNullOrWhiteSpace(x.PassportNumber) || x.ExperienceYears < 0 || x.BirthDate > DateOnly.FromDateTime(DateTime.Today)) throw new ArgumentException("Invalid veterinarian");
        var e = id.HasValue ? veterinarians.Get(id.Value) ?? throw new KeyNotFoundException() : new Veterinarian { Id = veterinarians.NextId() };
        e.FirstName = x.FirstName; e.LastName = x.LastName; e.Patronymic = x.Patronymic; e.Phone = x.Phone; e.PassportNumber = x.PassportNumber; e.BirthDate = x.BirthDate; e.Specialization = spec; e.ExperienceYears = x.ExperienceYears;
        if (!id.HasValue) veterinarians.Add(e);
        return DtoMapper.Map(e);
    }
    public void DeleteVeterinarian(int id) { if (appointments.GetAll().Any(x => x.Veterinarian.Id == id)) throw new InvalidOperationException("Veterinarian has appointments"); veterinarians.Remove(id); }
}
