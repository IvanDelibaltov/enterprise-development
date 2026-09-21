namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Запись питомца на приём к ветеринару.
/// </summary>
public class Appointment
{
    /// <summary>
    /// Идентификатор записи.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Питомец.
    /// </summary>
    public required Pet Pet { get; init; }

    /// <summary>
    /// Ветеринар.
    /// </summary>
    public required Veterinarian Veterinarian { get; init; }

    /// <summary>
    /// Дата и время приёма.
    /// </summary>
    public required DateTime AppointmentDate { get; init; }

    /// <summary>
    /// Номер кабинета.
    /// </summary>
    public required int RoomNumber { get; init; }

    /// <summary>
    /// Признак повторного приёма.
    /// </summary>
    public required bool IsRepeated { get; init; }
}
