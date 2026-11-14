namespace VeterinaryClinic.Application.Contracts.Veterinarians;

/// <summary>
/// Операции сервиса ветеринаров
/// </summary>
public interface IVeterinarianService
{
    /// <summary>
    /// Возвращает список ветеринаров клиники.
    /// </summary>
    Task<IReadOnlyList<VeterinarianDto>> VeterinariansAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Находит ветеринара по идентификатору.
    /// </summary>
    Task<VeterinarianDto?> GetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Создаёт ветеринара на основе переданных данных.
    /// </summary>
    Task<VeterinarianDto> CreateAsync(SaveVeterinarianRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Обновляет ветеринара по идентификатору.
    /// </summary>
    Task<VeterinarianDto> UpdateAsync(int id, SaveVeterinarianRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Удаляет ветеринара по идентификатору.
    /// </summary>
    Task DeleteVeterinarianAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает ветеринаров, работающих с указанным видом животных.
    /// </summary>
    Task<IReadOnlyList<VeterinarianDto>> VeterinariansBySpeciesAsync(VeterinaryClinic.Domain.Shared.Enums.AnimalType species, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает животных, обслуживаемых указанным ветеринаром.
    /// </summary>
    Task<IReadOnlyList<PetDto>> PetsByVeterinarianAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает записи на приём к указанному ветеринару.
    /// </summary>
    Task<IReadOnlyList<AppointmentDto>> AppointmentsByVeterinarianAsync(int id, CancellationToken cancellationToken = default);
}
