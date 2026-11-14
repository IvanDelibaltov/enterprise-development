namespace VeterinaryClinic.Application.Contracts.Owners;

/// <summary>
/// Операции сервиса владельцев
/// </summary>
public interface IOwnerService
{
    /// <summary>
    /// Возвращает список владельцев животных.
    /// </summary>
    Task<IReadOnlyList<OwnerDto>> OwnersAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Находит владельца по идентификатору.
    /// </summary>
    Task<OwnerDto?> GetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Создаёт владельца на основе переданных данных.
    /// </summary>
    Task<OwnerDto> CreateAsync(SaveOwnerRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Обновляет владельца по идентификатору.
    /// </summary>
    Task<OwnerDto> UpdateAsync(int id, SaveOwnerRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Удаляет владельца по идентификатору.
    /// </summary>
    Task DeleteOwnerAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает владельцев, у которых зарегистрировано несколько животных.
    /// </summary>
    Task<IReadOnlyList<OwnerDto>> OwnersWithMultiplePetsAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает животных указанного владельца.
    /// </summary>
    Task<IReadOnlyList<PetDto>> PetsByOwnerAsync(int id, CancellationToken cancellationToken = default);
}
