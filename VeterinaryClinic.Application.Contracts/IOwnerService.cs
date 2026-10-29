namespace VeterinaryClinic.Application.Contracts;

/// <summary>Операции сервиса владельцев</summary>
public interface IOwnerService
{
    /// <summary>Возвращает всех владельцев</summary>
    IReadOnlyList<OwnerDto> Owners();
    /// <summary>Возвращает владельца по идентификатору</summary>
    OwnerDto? Get(int id);
    /// <summary>Создаёт владельца</summary>
    OwnerDto Create(SaveOwnerRequest request);
    /// <summary>Обновляет владельца по идентификатору</summary>
    OwnerDto Update(int id, SaveOwnerRequest request);
    /// <summary>Удаляет владельца по идентификатору</summary>
    void DeleteOwner(int id);
    /// <summary>Возвращает владельцев нескольких питомцев</summary>
    IReadOnlyList<OwnerDto> OwnersWithMultiplePets();
    /// <summary>Возвращает питомцев владельца</summary>
    IReadOnlyList<PetDto> PetsByOwner(int id);
}
