using VeterinaryClinic.Domain.Shared.Enums;
namespace VeterinaryClinic.Application.Contracts;

public record BreedDto(int Id, string Name, AnimalType AnimalType);
public record SaveBreedRequest(string Name, AnimalType AnimalType);
