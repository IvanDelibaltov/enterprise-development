using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Выполняет операции с ресурсом «владелец»
/// </summary>
public sealed class OwnerService(IRepository<Owner> owners, IRepository<Pet> pets) : IOwnerService
{
    private static void Validate(SaveOwnerRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.FirstName) || string.IsNullOrWhiteSpace(x.LastName) ||
            string.IsNullOrWhiteSpace(x.Phone) || string.IsNullOrWhiteSpace(x.Address))
            throw new ArgumentException("Owner fields are required");
    }

    public async Task<IReadOnlyList<OwnerDto>> OwnersAsync(CancellationToken cancellationToken = default) => (await owners.GetAllAsync(cancellationToken)).Select(DtoMapper.Map).ToArray();

    public async Task<OwnerDto?> GetAsync(int id, CancellationToken cancellationToken = default) => (await owners.GetAsync(id, cancellationToken)) is { } o ? DtoMapper.Map(o) : null;

    public async Task<OwnerDto> CreateAsync(SaveOwnerRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        return DtoMapper.Map((await owners.CreateAsync(id => new Owner
        {
            Id = id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Patronymic = x.Patronymic,
            Phone = x.Phone,
            Address = x.Address
        }, cancellationToken)));
    }


    public async Task<OwnerDto> UpdateAsync(int id, SaveOwnerRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        return DtoMapper.Map((await owners.UpdateAsync(id, o =>
        {
            o.FirstName = x.FirstName; o.LastName = x.LastName;
            o.Patronymic = x.Patronymic; o.Phone = x.Phone; o.Address = x.Address;
        }, cancellationToken)));
    }


    public async Task DeleteOwnerAsync(int id, CancellationToken cancellationToken = default)
    {

        var owner = (await owners.GetAsync(id, cancellationToken));
        if (owner is null) return;
        foreach (var pet in (await pets.FindAsync(p => p.Owner != null && p.Owner.Id == id, cancellationToken))) pet.ChangeOwner(null);
        owner.Pets.Clear();
        await owners.RemoveAsync(id, cancellationToken);
    }


    public async Task<IReadOnlyList<OwnerDto>> OwnersWithMultiplePetsAsync(CancellationToken cancellationToken = default)
    {
        var allOwners = await owners.GetAllAsync(cancellationToken);
        var allPets = await pets.GetAllAsync(cancellationToken);
        return allOwners.Where(o => allPets.Count(p => p.Owner?.Id == o.Id) > 1)
            .OrderBy(o => o.LastName).ThenBy(o => o.FirstName).ThenBy(o => o.Patronymic)
            .Select(DtoMapper.Map).ToArray();
    }


    public async Task<IReadOnlyList<PetDto>> PetsByOwnerAsync(int id, CancellationToken cancellationToken = default)
    {

        if ((await owners.GetAsync(id, cancellationToken)) is null) throw new KeyNotFoundException($"Owner {id} not found");
        return (await pets.FindAsync(p => p.Owner != null && p.Owner.Id == id, cancellationToken)).Select(DtoMapper.Map).ToArray();
    }
}
