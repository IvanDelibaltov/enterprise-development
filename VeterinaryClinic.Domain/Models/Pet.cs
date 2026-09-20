namespace VeterinaryClinic.Domain.Models;

using VeterinaryClinic.Domain.Shared.Enums;

/// <summary>
/// Питомец.
/// </summary>
public class Pet
{
    /// <summary>
    /// Идентификатор питомца.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Кличка.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Вид животного.
    /// </summary>
    public required AnimalType AnimalType { get; init; }

    /// <summary>
    /// Порода.
    /// </summary>
    public required Breed Breed { get; init; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public required DateTime BirthDate { get; init; }

    /// <summary>
    /// Вес в килограммах.
    /// </summary>
    public required decimal Weight { get; init; }

    /// <summary>
    /// Владелец.
    /// </summary>
    public required Owner Owner { get; init; }
}
