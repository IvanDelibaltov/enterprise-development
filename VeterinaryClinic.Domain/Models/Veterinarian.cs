namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Ветеринар.
/// </summary>
public class Veterinarian
{
    /// <summary>
    /// Идентификатор ветеринара.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Номер паспорта.
    /// </summary>
    public required string PassportNumber { get; init; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// Имя.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; init; }

    /// <summary>
    /// Год рождения.
    /// </summary>
    public required int BirthYear { get; init; }

    /// <summary>
    /// Специализация.
    /// </summary>
    public required Specialization Specialization { get; init; }

    /// <summary>
    /// Стаж работы в годах.
    /// </summary>
    public required int ExperienceYears { get; init; }
}
