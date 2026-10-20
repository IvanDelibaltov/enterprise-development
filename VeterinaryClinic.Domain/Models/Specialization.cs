using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Models;
/// <summary>
/// Специализация ветеринарного врача
/// </summary>
public class Specialization
{
    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название специализации
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Вид животных, с которыми работает специалист
    /// </summary>
    public AnimalType AnimalType { get; set; }
}
