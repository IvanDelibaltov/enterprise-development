using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Commands;

/// <summary>Данные для сохранения специализации</summary>
public sealed record SpecializationCommand(string Name, AnimalType AnimalType);
