namespace VeterinaryClinic.Application.Contracts.Breeds;

/// <summary>
/// Операции сервиса пород
/// </summary>
public interface IBreedService
{
    /// <summary>
    /// Возвращает список пород животных.
    /// </summary>
    Task<IReadOnlyList<BreedDto>> BreedsAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Находит породу по идентификатору.
    /// </summary>
    Task<BreedDto?> GetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Создаёт породу на основе переданных данных.
    /// </summary>
    Task<BreedDto> CreateAsync(SaveBreedRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Обновляет породу по идентификатору.
    /// </summary>
    Task<BreedDto> UpdateAsync(int id, SaveBreedRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Удаляет породу по идентификатору.
    /// </summary>
    Task DeleteBreedAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Подсчитывает повторные приёмы животных указанной породы.
    /// </summary>
    Task<int> RepeatedAppointmentsAsync(int breedId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает животных указанной породы.
    /// </summary>
    Task<IReadOnlyList<PetDto>> PetsByBreedAsync(int id, CancellationToken cancellationToken = default);
}
