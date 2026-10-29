using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Commands;

/// <summary>Данные для сохранения ветеринара</summary>
public sealed record VeterinarianCommand(string LastName, string FirstName, string? Patronymic, string Phone,
    string PassportNumber,
    DateOnly BirthDate, int SpecializationId, int ExperienceYears);
