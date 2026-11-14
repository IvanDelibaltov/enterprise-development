using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Выполняет операции с ресурсом «питомец»
/// </summary>
public sealed class PetService(IRepository<Pet> pets, IRepository<Breed> breeds, IRepository<Owner> owners, IRepository<Appointment> appointments) : IPetService
{
    private static void Validate(SavePetRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name) || x.Weight <= 0 || !double.IsFinite(x.Weight) ||
            x.BirthDate > DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentException("Invalid pet");
    }

    public async Task<IReadOnlyList<PetDto>> PetsAsync(CancellationToken cancellationToken = default) => (await pets.GetAllAsync(cancellationToken)).Select(DtoMapper.Map).ToArray();

    public async Task<PetDto?> GetAsync(int id, CancellationToken cancellationToken = default) => (await pets.GetAsync(id, cancellationToken)) is { } p ? DtoMapper.Map(p) : null;

    public async Task<PetDto> CreateAsync(SavePetRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        var breed = (await breeds.GetAsync(x.BreedId, cancellationToken)) ?? throw new ArgumentException("Breed not found");
        var owner = await ResolveOwnerAsync(x.OwnerId, cancellationToken);
        var saved = (await pets.CreateAsync(id =>
        {
            var p = new Pet
            {
                Id = id,
                Name = x.Name,
                Breed = breed,
                BirthDate = x.BirthDate,
                Weight = x.Weight
            }; p.ChangeOwner(owner); return p;
        }, cancellationToken));
        return DtoMapper.Map(saved);
    }


    public async Task<PetDto> UpdateAsync(int id, SavePetRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        var breed = (await breeds.GetAsync(x.BreedId, cancellationToken)) ?? throw new ArgumentException("Breed not found");
        var owner = await ResolveOwnerAsync(x.OwnerId, cancellationToken);
        if ((await appointments.AnyAsync(a => a.Pet.Id == id && a.Veterinarian.Specialization.AnimalType != breed.AnimalType, cancellationToken)))
            throw new InvalidOperationException("Breed is incompatible with existing appointments");
        return DtoMapper.Map((await pets.UpdateAsync(id, p =>
        {
            p.Name = x.Name; p.Breed = breed;
            p.Weight = x.Weight; p.BirthDate = x.BirthDate; p.ChangeOwner(owner);
        }, cancellationToken)));
    }


    public async Task DeletePetAsync(int id, CancellationToken cancellationToken = default)
    {

        var pet = (await pets.GetAsync(id, cancellationToken));
        if (pet is null) return;
        if ((await appointments.AnyAsync(a => a.Pet.Id == id, cancellationToken))) throw new InvalidOperationException("Pet has appointments");
        pet.ChangeOwner(null);
        await pets.RemoveAsync(id, cancellationToken);
    }


    public async Task<IReadOnlyList<AppointmentDto>> AppointmentsByPetAsync(int id, CancellationToken cancellationToken = default)
    {

        if ((await pets.GetAsync(id, cancellationToken)) is null) throw new KeyNotFoundException($"Pet {id} not found");
        return (await appointments.FindAsync(a => a.Pet.Id == id, cancellationToken)).OrderBy(a => a.AppointmentDate)
            .Select(DtoMapper.Map).ToArray();
    }


    public async Task<OwnerDto?> OwnerByPetAsync(int id, CancellationToken cancellationToken = default)
    {

        var pet = (await pets.GetAsync(id, cancellationToken)) ?? throw new KeyNotFoundException($"Pet {id} not found");
        return pet.Owner is { } o ? DtoMapper.Map(o) : null;
    }

    private async Task<Owner?> ResolveOwnerAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is not int ownerId) return null;
        return await owners.GetAsync(ownerId, cancellationToken) ?? throw new ArgumentException("Owner not found");
    }
}
