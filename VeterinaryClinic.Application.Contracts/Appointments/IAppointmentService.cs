namespace VeterinaryClinic.Application.Contracts.Appointments;

/// <summary>
/// Операции сервиса приёмов
/// </summary>
public interface IAppointmentService
{
    /// <summary>
    /// Возвращает все записи на приём.
    /// </summary>
    Task<IReadOnlyList<AppointmentDto>> AppointmentsAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Находит приём по идентификатору.
    /// </summary>
    Task<AppointmentDto?> GetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Создаёт приём на основе переданных данных.
    /// </summary>
    Task<AppointmentDto> CreateAsync(SaveAppointmentRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Обновляет приём по идентификатору.
    /// </summary>
    Task<AppointmentDto> UpdateAsync(int id, SaveAppointmentRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Удаляет приём по идентификатору.
    /// </summary>
    Task DeleteAppointmentAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает записи на приём за указанный месяц и для заданного кабинета.
    /// </summary>
    Task<IReadOnlyList<AppointmentDto>> AppointmentsThisMonthAsync(int room, DateTime today, CancellationToken cancellationToken = default);
}
