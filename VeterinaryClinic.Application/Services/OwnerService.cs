using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

public sealed class OwnerService(IRepository<Owner> owners, IRepository<Pet> pets)
{
    public IReadOnlyList<OwnerDto> Owners() => owners.GetAll().Select(DtoMapper.Map).ToArray();
    public IReadOnlyList<OwnerDto> OwnersWithMultiplePets() => owners.GetAll()
        .Where(x => pets.GetAll().Count(p => p.Owner?.Id == x.Id) > 1)
        .OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ThenBy(x => x.Patronymic).Select(DtoMapper.Map).ToArray();
    public OwnerDto Save(int? id, SaveOwnerRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.FirstName) || string.IsNullOrWhiteSpace(x.LastName) || string.IsNullOrWhiteSpace(x.Phone) || string.IsNullOrWhiteSpace(x.Address)) throw new ArgumentException("Owner fields are required");
        var e = id.HasValue ? owners.Get(id.Value) ?? throw new KeyNotFoundException() : new Owner { Id = owners.NextId() };
        e.FirstName = x.FirstName; e.LastName = x.LastName; e.Patronymic = x.Patronymic; e.Phone = x.Phone; e.Address = x.Address;
        if (!id.HasValue) owners.Add(e);
        return DtoMapper.Map(e);
    }
    public void DeleteOwner(int id)
    {
        var owner = owners.Get(id);
        if (owner is null) return;
        foreach (var pet in pets.GetAll().Where(x => x.Owner?.Id == id))
        {
            pet.Owner = null;
        }
        owner.Pets.Clear();
        owners.Remove(id);
    }
}
