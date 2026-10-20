using VeterinaryClinic.Domain.Shared.Enums;
namespace VeterinaryClinic.Application.Contracts;

public record SpecializationDto(int Id, string Name, AnimalType AnimalType);
public record SaveSpecializationRequest(string Name, AnimalType AnimalType);
