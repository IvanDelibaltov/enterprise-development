using VeterinaryClinic.Domain.Shared.Enums;
namespace VeterinaryClinic.Application.Contracts;

/// <summary>
/// Данные специализации
/// </summary>
public record SpecializationDto(int Id, string Name, AnimalType AnimalType);
/// <summary>
/// Данные для создания или обновления специализации
/// </summary>
public record SaveSpecializationRequest(string Name, AnimalType AnimalType);
