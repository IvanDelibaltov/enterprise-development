using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Commands;

/// <summary>Данные для сохранения породы</summary>
public sealed record BreedCommand(string Name, AnimalType AnimalType);
