using VeterinaryClinic.Domain.Shared.Enums;
namespace VeterinaryClinic.Application.Contracts;

/// <summary>
/// Данные ветеринара
/// </summary>
public record VeterinarianDto(int Id, string LastName, string FirstName, string? Patronymic, string Phone, string PassportNumber, DateOnly BirthDate, int SpecializationId, int ExperienceYears);
/// <summary>
/// Данные для создания или обновления ветеринара
/// </summary>
public record SaveVeterinarianRequest(string LastName, string FirstName, string? Patronymic, string Phone, string PassportNumber, DateOnly BirthDate, int SpecializationId, int ExperienceYears);
