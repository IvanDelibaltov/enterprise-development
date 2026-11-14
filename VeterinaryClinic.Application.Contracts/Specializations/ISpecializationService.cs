namespace VeterinaryClinic.Application.Contracts.Specializations;

/// <summary>
/// Операции сервиса специализаций
/// </summary>
public interface ISpecializationService
{
    /// <summary>
    /// Возвращает список специализаций ветеринаров.
    /// </summary>
    Task<IReadOnlyList<SpecializationDto>> SpecializationsAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Находит специализацию по идентификатору.
    /// </summary>
    Task<SpecializationDto?> GetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Создаёт специализацию на основе переданных данных.
    /// </summary>
    Task<SpecializationDto> CreateAsync(SaveSpecializationRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Обновляет специализацию по идентификатору.
    /// </summary>
    Task<SpecializationDto> UpdateAsync(int id, SaveSpecializationRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Удаляет специализацию по идентификатору.
    /// </summary>
    Task DeleteSpecializationAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает ветеринаров заданной специализации.
    /// </summary>
    Task<IReadOnlyList<VeterinarianDto>> VeterinariansBySpecializationAsync(int id, CancellationToken cancellationToken = default);
}
