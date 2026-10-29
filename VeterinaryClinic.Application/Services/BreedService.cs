using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Сервис пород
/// </summary>
public sealed class BreedService(IRepository<Breed> breeds,
    IBreedRepository repository) : IBreedService
{
    /// <summary>
    /// Возвращает все породы
    /// </summary>
    public IReadOnlyList<BreedDto> Breeds() => breeds.GetAll().Select(DtoMapper.Map).ToArray();
    public int RepeatedAppointments(int breedId) => repository.RepeatedAppointments(breedId);
    /// <summary>Возвращает породу по идентификатору</summary>
    public BreedDto? Get(int id) => breeds.Get(id) is { } entity ? DtoMapper.Map(entity) : null;

    /// <summary>Создаёт породу</summary>
    public BreedDto Create(SaveBreedRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name)
            || !Enum.IsDefined(x.AnimalType)) throw new ArgumentException("Invalid breed");
        var saved = repository.CreateBreed(new BreedCommand(x.Name, x.AnimalType));
        return DtoMapper.Map(saved);
    }

    /// <summary>Обновляет породу</summary>
    public BreedDto Update(int id, SaveBreedRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name)
            || !Enum.IsDefined(x.AnimalType)) throw new ArgumentException("Invalid breed");
        var saved = repository.UpdateBreed(id, new BreedCommand(x.Name, x.AnimalType));
        return DtoMapper.Map(saved);
    }

    public void DeleteBreed(int id) => repository.DeleteBreed(id);
    /// <summary>Возвращает питомцев выбранной породы</summary>
    public IReadOnlyList<PetDto> PetsByBreed(int id)
    {
        return repository.PetsByBreed(id).Select(DtoMapper.Map).ToArray();
    }
}
