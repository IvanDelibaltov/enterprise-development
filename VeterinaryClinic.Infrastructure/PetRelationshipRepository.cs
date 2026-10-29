using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Infrastructure;

/// <summary>Поддерживает связь питомцев и владельцев</summary>
public sealed class PetRelationshipRepository(IRepository<Pet> pets) : IPetRelationshipRepository
{
    /// <inheritdoc/>
    public void AssignOwner(Pet pet, Owner? owner)
    {
        if (!ReferenceEquals(pet.Owner, owner))
        {
            pet.Owner?.Pets.Remove(pet);
            pet.Owner = owner;
        }
        if (owner is not null && !owner.Pets.Contains(pet))
            owner.Pets.Add(pet);
    }

    /// <inheritdoc/>
    public void UnlinkOwner(Owner owner)
    {
        foreach (var pet in pets.Find(p => p.Owner?.Id == owner.Id))
            AssignOwner(pet, null);
        owner.Pets.Clear();
    }
}
