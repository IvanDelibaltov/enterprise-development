using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Выполняет операции с ресурсом «порода»
/// </summary>
public sealed class BreedService(IRepository<Breed> breeds, IRepository<Pet> pets, IRepository<Appointment> appointments) : IBreedService
{
    private static void Validate(SaveBreedRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name) || !Enum.IsDefined(x.AnimalType))
            throw new ArgumentException("Invalid breed");
    }

    public async Task<IReadOnlyList<BreedDto>> BreedsAsync(CancellationToken cancellationToken = default) => (await breeds.GetAllAsync(cancellationToken)).Select(DtoMapper.Map).ToArray();

    public async Task<BreedDto?> GetAsync(int id, CancellationToken cancellationToken = default) => (await breeds.GetAsync(id, cancellationToken)) is { } b ? DtoMapper.Map(b) : null;

    public async Task<BreedDto> CreateAsync(SaveBreedRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        return DtoMapper.Map((await breeds.CreateAsync(id => new Breed { Id = id, Name = x.Name.Trim(), AnimalType = x.AnimalType }, cancellationToken)));
    }


    public async Task<BreedDto> UpdateAsync(int id, SaveBreedRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        var current = (await breeds.GetAsync(id, cancellationToken)) ?? throw new KeyNotFoundException($"Breed {id} not found");
        if (current.AnimalType != x.AnimalType && (await pets.AnyAsync(p => p.Breed.Id == id, cancellationToken)))
            throw new InvalidOperationException("Cannot change species of a breed used by pets");
        return DtoMapper.Map((await breeds.UpdateAsync(id, b => { b.Name = x.Name.Trim(); b.AnimalType = x.AnimalType; }, cancellationToken)));
    }


    public async Task DeleteBreedAsync(int id, CancellationToken cancellationToken = default)
    {

        if ((await pets.AnyAsync(p => p.Breed.Id == id, cancellationToken))) throw new InvalidOperationException("Breed is in use");
        await breeds.RemoveAsync(id, cancellationToken);
    }


    public async Task<int> RepeatedAppointmentsAsync(int breedId, CancellationToken cancellationToken = default)
    {

        if ((await breeds.GetAsync(breedId, cancellationToken)) is null) throw new KeyNotFoundException($"Breed {breedId} not found");
        return (await appointments.CountAsync(a => a.IsRepeated && a.Pet.Breed.Id == breedId, cancellationToken));
    }


    public async Task<IReadOnlyList<PetDto>> PetsByBreedAsync(int id, CancellationToken cancellationToken = default)
    {

        if ((await breeds.GetAsync(id, cancellationToken)) is null) throw new KeyNotFoundException($"Breed {id} not found");
        return (await pets.FindAsync(p => p.Breed.Id == id, cancellationToken)).Select(DtoMapper.Map).ToArray();
    }
}
