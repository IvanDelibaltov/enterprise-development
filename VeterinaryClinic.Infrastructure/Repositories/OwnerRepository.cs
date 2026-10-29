using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Infrastructure.Repositories;

/// <summary>Сохраняет владельцев и поддерживает связи</summary>
public sealed class OwnerRepository(
    IRepository<Owner> owners,
    IPetRelationshipRepository relationships,
    IRepository<Pet> pets) : IOwnerRepository
{
    /// <inheritdoc/>
    public Owner CreateOwner(OwnerCommand x)
    {
        return owners.Create(newId =>
        {
            var entity = new Owner { Id = newId };
            entity.FirstName = x.FirstName;
            entity.LastName = x.LastName;
            entity.Patronymic = x.Patronymic;
            entity.Phone = x.Phone;
            entity.Address = x.Address;
            return entity;
        });
    }

    /// <inheritdoc/>
    public Owner UpdateOwner(int id, OwnerCommand x)
    {
        return owners.Update(id, entity =>
        {
            entity.FirstName = x.FirstName;
            entity.LastName = x.LastName;
            entity.Patronymic = x.Patronymic;
            entity.Phone = x.Phone;
            entity.Address = x.Address;
        });
    }

    /// <inheritdoc/>
    public void DeleteOwner(int id)
    {
        var owner = owners.Get(id);
        if (owner is null) return;
        relationships.UnlinkOwner(owner);
        owners.Remove(id);
    }


    /// <inheritdoc/>
    public IReadOnlyList<Owner> OwnersWithMultiplePets() =>
        owners.Find(o => pets.Count(p => p.Owner?.Id == o.Id) > 1)
            .OrderBy(o => o.LastName).ThenBy(o => o.FirstName).ThenBy(o => o.Patronymic).ToArray();

    /// <inheritdoc/>
    public IReadOnlyList<Pet> PetsByOwner(int id)
    {
        if (owners.Get(id) is null) throw new KeyNotFoundException($"Owner {id} not found");
        return pets.Find(p => p.Owner?.Id == id);
    }
}
