namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Питомец
/// </summary>
public class Pet
{
    /// <summary>
    /// Идентификатор питомца
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Кличка
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Порода, определяющая вид животного
    /// </summary>
    public Breed Breed { get; set; } = null!;

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Вес в килограммах
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// Владелец, если он известен
    /// </summary>
    public Owner? Owner { get; set; }
}
