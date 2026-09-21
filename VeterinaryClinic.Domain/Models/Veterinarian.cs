namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Ветеринар
/// </summary>
public class Veterinarian
{
    /// <summary>
    /// Идентификатор ветеринара
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Год рождения
    /// </summary>
    public required int BirthYear { get; set; }

    /// <summary>
    /// Специализация
    /// </summary>
    public required Specialization Specialization { get; set; }

    /// <summary>
    /// Стаж работы в годах
    /// </summary>
    public required int ExperienceYears { get; set; }
}
