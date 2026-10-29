using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Сервис владельцев
/// </summary>
public sealed class OwnerService(IRepository<Owner> owners,
    IOwnerRepository repository) : IOwnerService
{
    /// <summary>
    /// Возвращает всех владельцев
    /// </summary>
    public IReadOnlyList<OwnerDto> Owners() => owners.GetAll().Select(DtoMapper.Map).ToArray();
    public IReadOnlyList<OwnerDto> OwnersWithMultiplePets()
        => repository.OwnersWithMultiplePets().Select(DtoMapper.Map).ToArray();
    /// <summary>Возвращает владельца по идентификатору</summary>
    public OwnerDto? Get(int id) => owners.Get(id) is { } entity ? DtoMapper.Map(entity) : null;

    /// <summary>Создаёт владельца</summary>
    public OwnerDto Create(SaveOwnerRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.FirstName) || string.IsNullOrWhiteSpace(x.LastName) ||
            string.IsNullOrWhiteSpace(x.Phone) || string.IsNullOrWhiteSpace(x.Address))
            throw new ArgumentException("Owner fields are required");
        var saved = repository.CreateOwner(new OwnerCommand(x.LastName, x.FirstName, x.Patronymic, x.Phone, x.Address));
        return DtoMapper.Map(saved);
    }

    /// <summary>Обновляет владельца</summary>
    public OwnerDto Update(int id, SaveOwnerRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.FirstName) || string.IsNullOrWhiteSpace(x.LastName) ||
            string.IsNullOrWhiteSpace(x.Phone) || string.IsNullOrWhiteSpace(x.Address))
            throw new ArgumentException("Owner fields are required");
        var saved = repository.UpdateOwner(id, new OwnerCommand(x.LastName, x.FirstName, x.Patronymic,
            x.Phone, x.Address));
        return DtoMapper.Map(saved);
    }

    public void DeleteOwner(int id) => repository.DeleteOwner(id);
    /// <summary>Возвращает питомцев владельца</summary>
    public IReadOnlyList<PetDto> PetsByOwner(int id)
    {
        return repository.PetsByOwner(id).Select(DtoMapper.Map).ToArray();
    }
}
