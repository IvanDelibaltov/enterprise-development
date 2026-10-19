namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Общие данные человека
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Отчество
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Телефон
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}
