namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Запись питомца на приём к ветеринару
/// </summary>
public class Appointment
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Питомец
    /// </summary>
    public required Pet Pet { get; set; }

    /// <summary>
    /// Ветеринар
    /// </summary>
    public required Veterinarian Veterinarian { get; set; }

    /// <summary>
    /// Дата и время приёма
    /// </summary>
    public required DateTime AppointmentDate { get; set; }

    /// <summary>
    /// Номер кабинета
    /// </summary>
    public required int RoomNumber { get; set; }

    /// <summary>
    /// Признак повторного приёма
    /// </summary>
    public required bool IsRepeated { get; set; }
}
