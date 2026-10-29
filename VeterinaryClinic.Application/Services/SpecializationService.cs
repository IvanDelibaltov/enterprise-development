using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Сервис специализаций
/// </summary>
public sealed class SpecializationService(IRepository<Specialization> specializations,
    ISpecializationRepository repository) : ISpecializationService
{
    /// <summary>
    /// Возвращает все специализации
    /// </summary>
    public IReadOnlyList<SpecializationDto> Specializations()
        => specializations.GetAll().Select(DtoMapper.Map).ToArray();
    /// <summary>Возвращает специализацию по идентификатору</summary>
    public SpecializationDto? Get(int id) => specializations.Get(id) is { } entity ? DtoMapper.Map(entity) : null;

    /// <summary>Создаёт специализацию</summary>
    public SpecializationDto Create(SaveSpecializationRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name)
            || !Enum.IsDefined(x.AnimalType)) throw new ArgumentException("Invalid specialization");
        var saved = repository.CreateSpecialization(new SpecializationCommand(x.Name, x.AnimalType));
        return DtoMapper.Map(saved);
    }

    /// <summary>Обновляет специализацию</summary>
    public SpecializationDto Update(int id, SaveSpecializationRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name)
            || !Enum.IsDefined(x.AnimalType)) throw new ArgumentException("Invalid specialization");
        var saved = repository.UpdateSpecialization(id, new SpecializationCommand(x.Name, x.AnimalType));
        return DtoMapper.Map(saved);
    }

    public void DeleteSpecialization(int id) => repository.DeleteSpecialization(id);
    /// <summary>Возвращает ветеринаров по специализации</summary>
    public IReadOnlyList<VeterinarianDto> VeterinariansBySpecialization(int id)
    {
        return repository.VeterinariansBySpecialization(id).Select(DtoMapper.Map).ToArray();
    }
}
