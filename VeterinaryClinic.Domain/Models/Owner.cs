namespace VeterinaryClinic.Domain.Models;
/// <summary>
/// Владелец одного или нескольких питомцев
/// </summary>
public class Owner : Person
{
    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Адрес владельца
    /// </summary>
    public string Address { get; set; } = string.Empty;
    /// <summary>
    /// Питомцы, связанные с владельцем
    /// </summary>
    public List<Pet> Pets { get; } = [];
}
