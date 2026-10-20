using VeterinaryClinic.Domain.Shared.Enums;
namespace VeterinaryClinic.Application.Contracts;

/// <summary>
/// Данные питомца
/// </summary>
public record PetDto(int Id, string Name, int BreedId, DateOnly BirthDate, double Weight, int? OwnerId);
/// <summary>
/// Данные для создания или обновления питомца
/// </summary>
public record SavePetRequest(string Name, int BreedId, DateOnly BirthDate, double Weight, int? OwnerId);
