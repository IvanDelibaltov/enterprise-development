using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Infrastructure.Repositories;

/// <summary>Сохраняет породы и проверяет связи</summary>
public sealed class BreedRepository(
    IRepository<Breed> breeds,
    IRepository<Pet> pets,
    IRepository<Appointment> appointments) : IBreedRepository
{
    /// <inheritdoc/>
    public Breed CreateBreed(BreedCommand x)
    {
        return breeds.Create(newId =>
        {
            var entity = new Breed { Id = newId };
            entity.Name = x.Name.Trim();
            entity.AnimalType = x.AnimalType;
            return entity;
        });
    }

    /// <inheritdoc/>
    public Breed UpdateBreed(int id, BreedCommand x)
    {
        var current = breeds.Get(id) ?? throw new KeyNotFoundException($"Breed {id} not found");
        if (current.AnimalType != x.AnimalType && pets.Any(p => p.Breed.Id == id))
            throw new InvalidOperationException("Cannot change species of a breed used by pets");
        return breeds.Update(id, entity =>
        {
            entity.Name = x.Name.Trim();
            entity.AnimalType = x.AnimalType;
        });
    }

    /// <inheritdoc/>
    public void DeleteBreed(int id)
    {
        if (pets.Any(p => p.Breed.Id == id))
            throw new InvalidOperationException("Breed is in use");
        breeds.Remove(id);
    }


    /// <inheritdoc/>
    public IReadOnlyList<Pet> PetsByBreed(int id)
    {
        if (breeds.Get(id) is null) throw new KeyNotFoundException($"Breed {id} not found");
        return pets.Find(p => p.Breed.Id == id);
    }

    /// <inheritdoc/>
    public int RepeatedAppointments(int breedId)
    {
        if (breeds.Get(breedId) is null) throw new KeyNotFoundException($"Breed {breedId} not found");
        return appointments.Count(a => a.IsRepeated && a.Pet.Breed.Id == breedId);
    }
}
