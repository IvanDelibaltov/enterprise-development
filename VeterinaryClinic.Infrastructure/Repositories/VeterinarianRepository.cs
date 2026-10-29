using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Infrastructure.Repositories;

/// <summary>Сохраняет ветеринаров и проверяет связи</summary>
public sealed class VeterinarianRepository(
    IRepository<Veterinarian> veterinarians,
    IRepository<Specialization> specializations,
    IRepository<Appointment> appointments) : IVeterinarianRepository
{
    /// <inheritdoc/>
    public Veterinarian CreateVeterinarian(VeterinarianCommand x)
    {
        var spec = specializations.Get(x.SpecializationId)
            ?? throw new ArgumentException("Specialization not found");
        return veterinarians.Create(newId =>
        {
            var entity = new Veterinarian { Id = newId, Specialization = spec };
            entity.FirstName = x.FirstName;
            entity.LastName = x.LastName;
            entity.Patronymic = x.Patronymic;
            entity.Phone = x.Phone;
            entity.PassportNumber = x.PassportNumber;
            entity.BirthDate = x.BirthDate;
            entity.ExperienceYears = x.ExperienceYears;
            return entity;
        });
    }

    /// <inheritdoc/>
    public Veterinarian UpdateVeterinarian(int id, VeterinarianCommand x)
    {
        var spec = specializations.Get(x.SpecializationId)
            ?? throw new ArgumentException("Specialization not found");
        if (appointments.Any(a => a.Veterinarian.Id == id && a.Pet.Breed.AnimalType != spec.AnimalType))
            throw new InvalidOperationException("Specialization is incompatible with existing appointments");
        return veterinarians.Update(id, entity =>
        {
            entity.FirstName = x.FirstName;
            entity.LastName = x.LastName;
            entity.Patronymic = x.Patronymic;
            entity.Phone = x.Phone;
            entity.PassportNumber = x.PassportNumber;
            entity.BirthDate = x.BirthDate;
            entity.Specialization = spec;
            entity.ExperienceYears = x.ExperienceYears;
        });
    }

    /// <inheritdoc/>
    public void DeleteVeterinarian(int id)
    {
        if (appointments.Any(a => a.Veterinarian.Id == id))
            throw new InvalidOperationException("Veterinarian has appointments");
        veterinarians.Remove(id);
    }


    /// <inheritdoc/>
    public IReadOnlyList<Veterinarian> VeterinariansBySpecies(AnimalType species) =>
        veterinarians.Find(v => v.Specialization.AnimalType == species);

    /// <inheritdoc/>
    public IReadOnlyList<Appointment> AppointmentsByVeterinarian(int id)
    {
        if (veterinarians.Get(id) is null)
            throw new KeyNotFoundException($"Veterinarian {id} not found");
        return appointments.Find(a => a.Veterinarian.Id == id)
            .OrderBy(a => a.AppointmentDate).ToArray();
    }

    /// <inheritdoc/>
    public IReadOnlyList<Pet> PetsByVeterinarian(int id)
    {
        if (veterinarians.Get(id) is null) throw new KeyNotFoundException($"Veterinarian {id} not found");
        return appointments.Find(a => a.Veterinarian.Id == id).Select(a => a.Pet)
            .DistinctBy(p => p.Id).OrderBy(p => p.Name).ToArray();
    }
}
