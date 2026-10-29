using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Commands;

/// <summary>Данные для сохранения питомца</summary>
public sealed record PetCommand(string Name, int BreedId, DateOnly BirthDate, double Weight, int? OwnerId);
