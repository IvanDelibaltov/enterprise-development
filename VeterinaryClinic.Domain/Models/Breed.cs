namespace VeterinaryClinic.Domain.Models;

using VeterinaryClinic.Domain.Shared.Enums;

/// <summary>
/// Порода животного.
/// </summary>
public class Breed
{
    /// <summary>
    /// Идентификатор породы.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Название породы.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Вид животного.
    /// </summary>
    public required AnimalType AnimalType { get; init; }
}
