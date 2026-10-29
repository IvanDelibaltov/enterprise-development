using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Infrastructure.Repositories;

/// <summary>Сохраняет питомцев и поддерживает связи</summary>
public sealed class PetRepository(
    IRepository<Pet> pets,
    IRepository<Breed> breeds,
    IRepository<Owner> owners,
    IRepository<Appointment> appointments,
    IPetRelationshipRepository relationships) : IPetRepository
{
    /// <inheritdoc/>
    public Pet CreatePet(PetCommand x)
    {
        var breed = breeds.Get(x.BreedId)
            ?? throw new ArgumentException("Breed not found");
        var owner = x.OwnerId is int ownerId
            ? owners.Get(ownerId) ?? throw new ArgumentException("Owner not found")
            : null;
        return pets.Create(newId =>
        {
            var entity = new Pet { Id = newId, Breed = breed };
            entity.Name = x.Name;
            entity.Weight = x.Weight;
            entity.BirthDate = x.BirthDate;
            relationships.AssignOwner(entity, owner);
            return entity;
        });
    }

    /// <inheritdoc/>
    public Pet UpdatePet(int id, PetCommand x)
    {
        var breed = breeds.Get(x.BreedId)
            ?? throw new ArgumentException("Breed not found");
        var owner = x.OwnerId is int ownerId
            ? owners.Get(ownerId) ?? throw new ArgumentException("Owner not found")
            : null;
        if (appointments.Any(a => a.Pet.Id == id && a.Veterinarian.Specialization.AnimalType != breed.AnimalType))
            throw new InvalidOperationException("Breed is incompatible with existing appointments");
        return pets.Update(id, entity =>
        {
            entity.Name = x.Name;
            entity.Breed = breed;
            entity.Weight = x.Weight;
            entity.BirthDate = x.BirthDate;
            relationships.AssignOwner(entity, owner);
        });
    }

    /// <inheritdoc/>
    public void DeletePet(int id)
    {
        var pet = pets.Get(id);
        if (pet is null) return;
        if (appointments.Any(a => a.Pet.Id == id))
            throw new InvalidOperationException("Pet has appointments");
        relationships.AssignOwner(pet, null);
        pets.Remove(id);
    }

    /// <inheritdoc/>
    public IReadOnlyList<Appointment> AppointmentsByPet(int petId)
    {
        if (pets.Get(petId) is null)
            throw new KeyNotFoundException($"Pet {petId} not found");
        return appointments.Find(a => a.Pet.Id == petId)
            .OrderBy(a => a.AppointmentDate).ToArray();
    }

    /// <inheritdoc/>
    public Owner? GetPetOwner(int petId)
    {
        var pet = pets.Get(petId) ?? throw new KeyNotFoundException($"Pet {petId} not found");
        return pet.Owner;
    }

}
