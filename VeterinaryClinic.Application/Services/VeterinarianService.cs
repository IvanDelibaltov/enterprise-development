using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Сервис ветеринаров
/// </summary>
public sealed class VeterinarianService(IRepository<Veterinarian> veterinarians,
    IVeterinarianRepository repository) : IVeterinarianService
{
    /// <summary>
    /// Возвращает всех ветеринаров
    /// </summary>
    public IReadOnlyList<VeterinarianDto> Veterinarians() => veterinarians.GetAll().Select(DtoMapper.Map).ToArray();
    public IReadOnlyList<VeterinarianDto> VeterinariansBySpecies(AnimalType species)
        => repository.VeterinariansBySpecies(species).Select(DtoMapper.Map).ToArray();
    public IReadOnlyList<PetDto> PetsByVeterinarian(int id)
        => repository.PetsByVeterinarian(id).Select(DtoMapper.Map).ToArray();
    /// <summary>Возвращает ветеринара по идентификатору</summary>
    public VeterinarianDto? Get(int id) => veterinarians.Get(id) is { } entity ? DtoMapper.Map(entity) : null;

    /// <summary>Создаёт ветеринара</summary>
    public VeterinarianDto Create(SaveVeterinarianRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.FirstName) || string.IsNullOrWhiteSpace(x.LastName) ||
            string.IsNullOrWhiteSpace(x.PassportNumber) || x.ExperienceYears < 0 ||
            x.BirthDate > DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentException("Invalid veterinarian");
        var saved = repository.CreateVeterinarian(new VeterinarianCommand(x.LastName, x.FirstName, x.Patronymic,
            x.Phone, x.PassportNumber, x.BirthDate, x.SpecializationId, x.ExperienceYears));
        return DtoMapper.Map(saved);
    }

    /// <summary>Обновляет ветеринара</summary>
    public VeterinarianDto Update(int id, SaveVeterinarianRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.FirstName) || string.IsNullOrWhiteSpace(x.LastName) ||
            string.IsNullOrWhiteSpace(x.PassportNumber) || x.ExperienceYears < 0 ||
            x.BirthDate > DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentException("Invalid veterinarian");
        var saved = repository.UpdateVeterinarian(id, new VeterinarianCommand(x.LastName, x.FirstName,
            x.Patronymic, x.Phone, x.PassportNumber, x.BirthDate, x.SpecializationId, x.ExperienceYears));
        return DtoMapper.Map(saved);
    }

    /// <summary>Возвращает приёмы ветеринара</summary>
    public IReadOnlyList<AppointmentDto> AppointmentsByVeterinarian(int id) =>
        repository.AppointmentsByVeterinarian(id).Select(DtoMapper.Map).ToArray();

    public void DeleteVeterinarian(int id) => repository.DeleteVeterinarian(id);
}
