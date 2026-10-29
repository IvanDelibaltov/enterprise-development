using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Сервис питомцев
/// </summary>
public sealed class PetService(IRepository<Pet> pets, IPetRepository repository) : IPetService
{
    /// <summary>
    /// Возвращает всех питомцев
    /// </summary>
    public IReadOnlyList<PetDto> Pets() => pets.GetAll().Select(DtoMapper.Map).ToArray();
    /// <summary>Возвращает питомца по идентификатору</summary>
    public PetDto? Get(int id) => pets.Get(id) is { } entity ? DtoMapper.Map(entity) : null;

    /// <summary>Создаёт питомца</summary>
    public PetDto Create(SavePetRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name) || x.Weight <= 0 || !double.IsFinite(x.Weight)
            || x.BirthDate > DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentException("Invalid pet");
        var saved = repository.CreatePet(new PetCommand(x.Name, x.BreedId, x.BirthDate, x.Weight, x.OwnerId));
        return DtoMapper.Map(saved);
    }

    /// <summary>Обновляет питомца</summary>
    public PetDto Update(int id, SavePetRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name) || x.Weight <= 0 || !double.IsFinite(x.Weight)
            || x.BirthDate > DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentException("Invalid pet");
        var saved = repository.UpdatePet(id, new PetCommand(x.Name, x.BreedId, x.BirthDate, x.Weight, x.OwnerId));
        return DtoMapper.Map(saved);
    }

    /// <summary>Возвращает приёмы питомца</summary>
    public IReadOnlyList<AppointmentDto> AppointmentsByPet(int id) =>
        repository.AppointmentsByPet(id).Select(DtoMapper.Map).ToArray();

    public void DeletePet(int id) => repository.DeletePet(id);
    /// <summary>Возвращает владельца питомца</summary>
    public OwnerDto? OwnerByPet(int id)
    {
        return repository.GetPetOwner(id) is { } owner ? DtoMapper.Map(owner) : null;
    }
}
