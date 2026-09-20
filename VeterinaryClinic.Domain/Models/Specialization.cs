namespace VeterinaryClinic.Domain.Models;

using VeterinaryClinic.Domain.Shared.Enums;

/// <summary>
/// Специализация ветеринара.
/// </summary>
public class Specialization
{
    /// <summary>
    /// Идентификатор специализации.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Название специализации.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Основной вид животных, с которым работает специалист.
    /// </summary>
    public required AnimalType AnimalType { get; init; }
}
