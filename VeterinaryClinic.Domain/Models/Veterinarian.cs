namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Ветеринар
/// </summary>
public class Veterinarian : Person
{
    /// <summary>
    /// Идентификатор ветеринара
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public string PassportNumber { get; set; } = string.Empty;

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Специализация
    /// </summary>
    public Specialization Specialization { get; set; } = null!;

    /// <summary>
    /// Стаж работы в годах
    /// </summary>
    public int ExperienceYears { get; set; }
}
