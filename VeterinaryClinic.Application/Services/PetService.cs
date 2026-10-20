using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

public sealed class PetService(IRepository<Pet> pets, IRepository<Breed> breeds, IRepository<Owner> owners, IRepository<Appointment> appointments)
{
    public IReadOnlyList<PetDto> Pets() => pets.GetAll().Select(DtoMapper.Map).ToArray();
    public PetDto Save(int? id, SavePetRequest x)
    {
        var breed = breeds.Get(x.BreedId) ?? throw new ArgumentException("Breed not found");
        var owner = x.OwnerId.HasValue ? owners.Get(x.OwnerId.Value) ?? throw new ArgumentException("Owner not found") : null;
        if (string.IsNullOrWhiteSpace(x.Name) || x.Weight <= 0 || !double.IsFinite(x.Weight) || x.BirthDate > DateOnly.FromDateTime(DateTime.Today)) throw new ArgumentException("Invalid pet");
        var e = id.HasValue ? pets.Get(id.Value) ?? throw new KeyNotFoundException() : new Pet { Id = pets.NextId() };
        var previousOwner = e.Owner;
        e.Name = x.Name;
        e.Breed = breed;
        e.Weight = x.Weight;
        e.BirthDate = x.BirthDate;

        if (!ReferenceEquals(previousOwner, owner))
        {
            previousOwner?.Pets.Remove(e);
            e.Owner = owner;
        }
        if (owner is not null && !owner.Pets.Contains(e)) owner.Pets.Add(e);
        if (!id.HasValue) pets.Add(e);
        return DtoMapper.Map(e);
    }
    public void DeletePet(int id)
    {
        var pet = pets.Get(id);
        if (pet is null) return;
        if (appointments.GetAll().Any(x => x.Pet.Id == id)) throw new InvalidOperationException("Pet has appointments");
        pet.Owner?.Pets.Remove(pet);
        pet.Owner = null;
        pets.Remove(id);
    }
}
