namespace VeterinaryClinic.Application.Contracts.Pets;

/// <summary>
/// Операции сервиса питомцев
/// </summary>
public interface IPetService
{
    /// <summary>
    /// Возвращает список животных клиники.
    /// </summary>
    Task<IReadOnlyList<PetDto>> PetsAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Находит животное по идентификатору.
    /// </summary>
    Task<PetDto?> GetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Создаёт животное на основе переданных данных.
    /// </summary>
    Task<PetDto> CreateAsync(SavePetRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Обновляет животное по идентификатору.
    /// </summary>
    Task<PetDto> UpdateAsync(int id, SavePetRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Удаляет животное по идентификатору.
    /// </summary>
    Task DeletePetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает владельца указанного животного.
    /// </summary>
    Task<OwnerDto?> OwnerByPetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает записи на приём указанного животного.
    /// </summary>
    Task<IReadOnlyList<AppointmentDto>> AppointmentsByPetAsync(int id, CancellationToken cancellationToken = default);
}
