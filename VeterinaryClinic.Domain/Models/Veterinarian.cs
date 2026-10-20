namespace VeterinaryClinic.Domain.Models;
/// <summary>
/// Ветеринарный врач
/// </summary>
public class Veterinarian : Person
{
    /// <summary>
    /// Идентификатор врача
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Номер паспорта врача
    /// </summary>
    public string PassportNumber { get; set; } = string.Empty;
    /// <summary>
    /// Дата рождения врача
    /// </summary>
    public DateOnly BirthDate { get; set; }
    /// <summary>
    /// Специализация врача и обслуживаемый вид животных
    /// </summary>
    public Specialization Specialization { get; set; } = null!;
    /// <summary>
    /// Стаж работы в полных годах
    /// </summary>
    public int ExperienceYears { get; set; }
}
