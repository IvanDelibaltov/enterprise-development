using VeterinaryClinic.Domain.Shared.Enums;
namespace VeterinaryClinic.Application.Contracts;

public record VeterinarianDto(int Id, string LastName, string FirstName, string? Patronymic, string Phone, string PassportNumber, DateOnly BirthDate, int SpecializationId, int ExperienceYears);
public record SaveVeterinarianRequest(string LastName, string FirstName, string? Patronymic, string Phone, string PassportNumber, DateOnly BirthDate, int SpecializationId, int ExperienceYears);
