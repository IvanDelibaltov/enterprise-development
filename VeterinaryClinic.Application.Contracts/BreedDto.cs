using VeterinaryClinic.Domain.Shared.Enums;
namespace VeterinaryClinic.Application.Contracts;

/// <summary>
/// Данные породы
/// </summary>
public record BreedDto(int Id, string Name, AnimalType AnimalType);
/// <summary>
/// Данные для создания или обновления породы
/// </summary>
public record SaveBreedRequest(string Name, AnimalType AnimalType);
