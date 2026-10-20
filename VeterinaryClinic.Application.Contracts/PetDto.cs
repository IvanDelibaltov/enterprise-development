using VeterinaryClinic.Domain.Shared.Enums;
namespace VeterinaryClinic.Application.Contracts;

public record PetDto(int Id, string Name, int BreedId, DateOnly BirthDate, double Weight, int? OwnerId);
public record SavePetRequest(string Name, int BreedId, DateOnly BirthDate, double Weight, int? OwnerId);
