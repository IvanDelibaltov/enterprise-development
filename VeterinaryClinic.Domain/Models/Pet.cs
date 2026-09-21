namespace VeterinaryClinic.Domain.Models;

using VeterinaryClinic.Domain.Shared.Enums;

/// <summary>
/// Питомец
/// </summary>
public class Pet
{
    /// <summary>
    /// Идентификатор питомца
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Кличка
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Вид животного
    /// </summary>
    public required AnimalType AnimalType { get; set; }

    /// <summary>
    /// Порода
    /// </summary>
    public required Breed Breed { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateTime BirthDate { get; set; }

    /// <summary>
    /// Вес в килограммах
    /// </summary>
    public required decimal Weight { get; set; }

    /// <summary>
    /// Владелец
    /// </summary>
    public required Owner Owner { get; set; }
}
