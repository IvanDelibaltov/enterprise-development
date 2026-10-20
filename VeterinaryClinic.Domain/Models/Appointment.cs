namespace VeterinaryClinic.Domain.Models;
/// <summary>
/// Запись питомца на приём к ветеринару
/// </summary>
public class Appointment
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Питомец, записанный на приём
    /// </summary>
    public Pet Pet { get; set; } = null!;
    /// <summary>
    /// Врач, ведущий приём
    /// </summary>
    public Veterinarian Veterinarian { get; set; } = null!;
    /// <summary>
    /// Дата и время приёма
    /// </summary>
    public DateTime AppointmentDate { get; set; }
    /// <summary>
    /// Номер кабинета
    /// </summary>
    public int RoomNumber { get; set; }
    /// <summary>
    /// Признак повторного приёма
    /// </summary>
    public bool IsRepeated { get; set; }
}
