namespace VeterinaryClinic.Domain.Models;
/// <summary>
/// Общие сведения о человеке
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
    /// Отчество, если имеется
    /// </summary>
    public string? Patronymic { get; set; }
    /// <summary>
    /// Контактный телефон
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}
