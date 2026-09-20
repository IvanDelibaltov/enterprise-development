namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Владелец питомцев.
/// </summary>
public class Owner
{
    /// <summary>
    /// Идентификатор владельца.
    /// </summary>
    public required int Id { get; init; }

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
    /// Адрес.
    /// </summary>
    public required string Address { get; init; }

    /// <summary>
    /// Телефон.
    /// </summary>
    public required string Phone { get; init; }

    /// <summary>
    /// Питомцы владельца.
    /// </summary>
    public List<Pet> Pets { get; } = [];
}
