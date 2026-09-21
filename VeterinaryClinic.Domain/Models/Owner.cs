namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Владелец питомцев
/// </summary>
public class Owner
{
    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public required int Id { get; set; }

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
    /// Адрес
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Телефон
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Питомцы владельца
    /// </summary>
    public List<Pet> Pets { get; } = [];
}
