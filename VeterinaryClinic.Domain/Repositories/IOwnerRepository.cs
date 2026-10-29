using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Repositories;

/// <summary>Операции с данными владельцев</summary>
public interface IOwnerRepository
{
    /// <summary>Создаёт владельца</summary>
    Owner CreateOwner(OwnerCommand request);
    /// <summary>Обновляет владельца</summary>
    Owner UpdateOwner(int id, OwnerCommand request);
    /// <summary>Удаляет владельца</summary>
    void DeleteOwner(int id);
    /// <summary>Возвращает владельцев с несколькими питомцами</summary>
    IReadOnlyList<Owner> OwnersWithMultiplePets();
    /// <summary>Возвращает питомцев владельца</summary>
    IReadOnlyList<Pet> PetsByOwner(int id);
}
